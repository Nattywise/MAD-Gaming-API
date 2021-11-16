The following instructions will assist in making sure that the app is running.

Environment setup
---------------------------------------------------------------------
-the application will require visual studio 2017 version 15.9.20
-SQL Server express
-Make sure that dotnet core sdk 2.2.110 is installed on your machine.

Application setup
---------------------------------------------------------------------
-create mad database in sql server
-run the script called [Data]found under scripts folder under MadDataAccess project. the script will create your tables and also insert data in the tables.
-update your connection string found in the WebHelper.cs class. under MadApi Project

To create model
--------------------------------------------------------------------------------
-Open nuget package manager console
-Install-Package Microsoft.EntityFrameworkCore.SqlServer
-Install-Package Microsoft.EntityFrameworkCore.Tools
-Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design

-Scaffold-DbContext "Server=localhost;Database=Mad;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model

-ReScaffold
-Scaffold-DbContext "Server=localhost;Database=Mad;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -Force

Clean and rebuild the application. and you may use the MadTest>>CompletitionTest.cs to test the functionaliy of the API.
