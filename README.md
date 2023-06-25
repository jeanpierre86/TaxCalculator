# TaxCalculator
Tax calculator solution for a technical assignment

Implemented using .NET 7.

The solution consists of multple projects namely WebApi, Core, Infrastructure and Web. The solution was implemented using clean architecture and SOLID princples.

To run the solution (without localdb provided):

Open the solution file **TaxCalculator.sln** in VS2022.
Open appsettings.json edit the default connection string to point to your localdb instance.
Open Package Manager Console and run command **Update-Database -verbose**. This will create the database based on your connection string.
Right-click the WebApi project, under Debug, Start New Instance. This will run the Web API project and should load up a swagger UI page.
Right-click the Web project, under Debug, Start New Instance. This will run the MVC Web Application.
