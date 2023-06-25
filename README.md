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

**Notes and Concerns**

The flat rates, flat values and progressive rates (tax brackets) are stored in the database. The benefit to storing these in the database is that they can can be edited later since tax rates and brackets may change in the future. The disadvantage of this approach is that every time a calculation is executed, the database will need to be hit to retrieve these values.

I have used decimal data types for the rates and amounts. The rates and the computed income tax decimals have higher precision in order to maximize accuracy. However making the precision too high may cause unnceccessarily large database tables in the future. I hence assumed that a scale of 4 decimal points should be the highest.

The initial seed data will store the flat rates, flat values, and progressive rates according to the examples provided in the assignment.

A note on the progressive rates table in the assignment:

The first bracket starts from 0, whilst the rest of the brackets start with a last digit of 1. For instance, the second bracket To specifies 33950 (0 to 8350 at 10%), whilst the third bracket 82250 (8351 to 33950 - 15%). The algorithm to compute the rates therefore needed to take that fact into account. I did not want to seed the progressive rates table with values other than what is indicated in the assignment.

I am still in the process of ironing some things out. For instance my Web project is faily simple, and I wanted to implemented an additional Web API method to return the available postal codes, and have the Web project UI provide a drop down list rather than typing in the postal codes. My validation and error handling is also somewhat incomplete and I would have preferred to iron out issues related to error handling. I have not placed heavy emphasis on styling and have opted to use bootstrap for demonstrative purposes.