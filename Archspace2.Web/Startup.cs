using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Archspace2.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            Configuration.GetSection("SqlServer").Bind(sqlConnectionStringBuilder);

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(sqlConnectionStringBuilder.ToString()));
            
            Game.InitializeAsync(sqlConnectionStringBuilder.ToString()).GetAwaiter().GetResult();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(MicrosoftAccountDefaults.AuthenticationScheme, options =>
            {
                Configuration.GetSection("OpenIdConnect").GetSection("Microsoft").Bind(options);
                options.Authority = "https://login.microsoftonline.com/common/v2.0/";
                options.Scope.Add(OpenIdConnectScope.OpenId);
                options.Scope.Add("profile");
                options.Scope.Add("offline_access");
                options.Scope.Add("email");
                options.Scope.Add("User.Read");
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                options.CallbackPath = "/authentication/microsoft";
                options.ResponseMode = OpenIdConnectResponseMode.FormPost;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "https://login.microsoftonline.com/9188040d-6c67-4c5b-b112-36a304b66dad/v2.0",
                    ValidateIssuer = true
                };

                options.Events = new OpenIdConnectEvents()
                {
                    OnAuthorizationCodeReceived = async (context) =>
                    {
                        HttpRequest request = context.Request;
                        ConfidentialClientApplication confidentialClientApplication = new ConfidentialClientApplication(
                            context.Options.ClientId,
                            context.Options.Authority,
                            UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path),
                            new ClientCredential(context.Options.ClientSecret),
                            null,
                            null);

                        AuthenticationResult result = await confidentialClientApplication.AcquireTokenByAuthorizationCodeAsync(context.ProtocolMessage.Code, context.Options.Scope.Except(new List<string>() { "openid", "profile", "offline_access" }));

                        try
                        {
                            await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, context.Principal);

                            context.HandleCodeRedemption(result.AccessToken, result.IdToken);
                        }
                        catch (Exception e)
                        {
                            await Game.LogAsync(e.ToString(), LogType.Error);
                        }
                    },
                    OnRemoteFailure = async (context) =>
                    {
                        await Game.LogAsync("Remote Failure");
                    },
                    OnTicketReceived = async (context) =>
                    {
                        await Game.AddOrUpdateUserAsync(context.Principal);
                    },
                    OnRedirectToIdentityProvider = async (context) =>
                    {
                        if (!context.Request.IsHttps)
                        {
                            context.Request.Scheme = "https";
                        }
                    }
                }
                ;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, async (options) =>
            {
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            if (true)
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
        }
    }
}
