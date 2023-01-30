# Archspace 2

## To Run
* Install the latest version of Visual Studio 2022 or later, choosing ASP.NET and web development in the features list.
* Install SQL server or another database provider. Alternatively use a cloud SQL service.
* Open Archspace2.sln.
* Right-click Archspace2.Web and click on "Manage User Secrets".
* Paste in these user secrets or similar. You might need to alter the connection string. The example one is for a default-named SQL Server instance installed locally. The Open ID connect details are considered non-privileged - you are free to use it for development.
* Set the startup project to Archspace2.Web and run using the button in the interface or by pressing F5.
```
{
  "OpenIdConnect": {
    "Microsoft": {
      "ClientId": "39409c74-022b-4530-92d4-1eedbe7fbd3c",
      "ClientSecret": "qbhoFGWNT4[escMU8755$]="
    }
  },
  "ConnectionStrings": {
    "Database": "Server=.;Initial Catalog=Archspace 2;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

## To Do
This was built circa 2018 in a couple of days. To make it maintainable:
* Strip out jQuery/Bootstrap.
* Upgrade to ES6 modules and web components and strip out the MVC components.
* Refactor to separate the domain/logic from the persistence models.
* Add migrations.
* Support custom user accounts so Microsoft Accounts aren't required.