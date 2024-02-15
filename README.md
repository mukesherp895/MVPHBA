# MVPHBA
How to run this project?
1. Download Visual Studio 2022 and .NET 8, MS SQL Server above 2016.
2. Change connectionstring in appsettings.json. It's locate in MVPHBA.WebAPI.
3. Go to Visual Studio Package Console Manager and run this command update-migration.
4. Run DBScript on your MS SQL. DBScript locate in MVPHBA.DataAccess->SQLScript.
5. Finally you can run this project.

Project Explain
MVPHBA.WebAPI -> Presentation Layer API doc for Third Party
MVPHBA.WebService -> Implement Business Logic Layer
MVPHBA.DataAccess -> Implement Database operation Layer
MVPHBA.Model -> Data Presentation Layer
MVPHBA.Common -> Implement Static class for all Layer
MVPHBA.UnitTest -> Implement all Leyer Unit Test
