Assuming your in this project directory. ""

Seeding instructions
1. Go to "CA.ERP.Utilities" folder and create file appsettings.local.json.
2. Copy content from appsettings.json to appsettings.local.json 
3. Change to your correct connection string.
4. Make sure your database schema is updated or the db it self doesn't exist.
5. Run seed by running "dotnet run /seed".


Migrate intrtuctions
1. Go to "CA.ERP.Utilities" folder and create file appsettings.local.json.
2. Copy content from appsettings.json to appsettings.local.json 
3. Set OldDbConnection connection string to source database.
4. Set DefaultConnection connection string to target database.
5. Target database must be empty. If the database doesn't exist a new one will be automatically created.
6. Run migration by running "dotnet run /migrate".