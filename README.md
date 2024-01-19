# User-Projects-TimeLogs

An app design to display users with different projects and the time invested in them.
App is still under development and has a lot of fetures missing.

# Set up

The app uses .Net Web Api for the Backend, Angular for the Frontend and MySql for a the DB.

### Setting up the Backend and MySql

1. Navigate to UserProject
   <br/>
   In the terminal: 
   ```
    cd backendV1/UserProject
   ```
2. Install the dependencies
 
   ```
    dotnet restore
   ```
4. Set up a connection string to MqSql DB
   <br/>
   Open the appsettings.json file and update the entry "ConnectionStrings" with a child entry for the MySQL connection string (e.g. "WebApiDatabase"),
   the connection string should be in the format "server=[DB SERVER URL]; database=[DB NAME]; user=[USERNAME]; password=[PASSWORD]".
5. Generate EF Core migrations
   <br/>
   In the terminal:
   ```
     dotnet ef migrations add InitialCreate
   ```
   This will create a Migrations folder that will contain the logic for creating the DB.
6. Execute EF Core migrations
   <br/>
   In the terminal:
   ```
     dotnet ef database update
   ```
   To execute the EF Core migrations and create the database and tables in MySQL.
7. To run
   <br/>
   In the terminal you can run ``` dotnet run ``` or pres ``` f5 ``` to start in debug mode.

