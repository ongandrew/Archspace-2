using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archspace2.Web
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
			WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

			IConfiguration configuration = webApplicationBuilder.Configuration;

			webApplicationBuilder
				.Services
				.AddMvc()
				.AddNewtonsoftJson();

			webApplicationBuilder
				.Services
				.AddDbContext<DatabaseContext>(options =>
				{
					options.UseSqlServer(configuration.GetConnectionString("Database"));
				}
			);

			Game.InitializeAsync(configuration.GetConnectionString("Database")).GetAwaiter().GetResult();

			webApplicationBuilder
				.Services
				.AddAuthentication(options =>
				{
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = MicrosoftAccountDefaults.AuthenticationScheme;
				})
				.AddOpenIdConnect(MicrosoftAccountDefaults.AuthenticationScheme, options =>
				{
					configuration.GetSection("OpenIdConnect").GetSection("Microsoft").Bind(options);
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

							IConfidentialClientApplication confidentialClientApplication = 
								ConfidentialClientApplicationBuilder.CreateWithApplicationOptions(new ConfidentialClientApplicationOptions()
								{
									ClientId = context.Options.ClientId,
									RedirectUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path),
									ClientSecret = context.Options.ClientSecret,
									TenantId = context.Options.Authority
								}).Build();
							

							AuthenticationResult result = await confidentialClientApplication
								.AcquireTokenByAuthorizationCode(context.Options.Scope.Except(new List<string>() { "openid", "profile", "offline_access" }), context.ProtocolMessage.Code)
								.ExecuteAsync();

							try
							{
								await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, context.Principal);

								context.HandleCodeRedemption(result.AccessToken, result.IdToken);
							}
							catch (Exception exception)
							{
								await Game.LogAsync(exception.ToString(), LogType.Error);
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
					};
				})
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, async (options) =>
				{
				});

			WebApplication webApplication = webApplicationBuilder.Build();

			if (!webApplication.Environment.IsDevelopment())
			{
				webApplication.UseExceptionHandler("/Error");
				webApplication.UseHsts();
			}

			webApplication.UseHttpsRedirection();
			webApplication.UseStaticFiles();

			webApplication.UseRouting();

			webApplication.UseAuthorization();

			webApplication.MapControllers();
			webApplication.MapRazorPages();

			await webApplication.RunAsync();
		}
    }
}
