# DB-Creator

This application is designed to create databases on existing database servers. Simply use a named connection string, and the app will create the corresponding database for it. You can use multiple connection strings at the same time but only 1 for each provider.

### Environment Variable
To configure the connection string, set an environment variable using the following schema:
```
ConnectionStrings__{ConnectionStringName}
```
Note: Use double underscore between "ConnectionStrings" and "{ConnectionStringName}"

Example of an environment variable for postgres:
```
ConnectionStrings__postgres="User ID=sa;Password=mypassword;Host=localhost;Port=5432;Database=myDataBase;"
```

### Docker Run Example
To run the application using Docker, you can use the following command:
```
docker run -e ConnectionStrings__postgres="User ID=sa;Password=mypassword;Host=localhost;Port=5432;Database=myDataBase;" cisecke/db-creator
```

## Provider
### Npgsql
Provider: Npgsql.EntityFrameworkCore.PostgreSQL  
Version: 9.0.3  
ConnectionStringName: postgres  
### SqlServer
Provider: Microsoft.EntityFrameworkCore.SqlServer  
Version: 9.0.2  
ConnectionStringName: sqlserver  
### MongoDB
Provider: MongoDB.EntityFrameworkCore  
Version: 8.2.3  
ConnectionStringName: mongodb  
DatabaseName: test_db  
### Oracle
Provider: Oracle.EntityFrameworkCore  
Version: 9.23.60  
ConnectionStringName: oracle  
### MySql / MariaDB
Provider: Pomelo.EntityFrameworkCore.MySql  
Version: 8.0.2  
ConnectionStringName: mysql  
### Interbase
Provider: InterBaseSql.EntityFrameworkCore.InterBase  
Version: 10.0.2  
ConnectionStringName: interbase  
### Firebird
Provider: FirebirdSql.EntityFrameworkCore.Firebird  
Version: 12.0.0  
ConnectionStringName: firebird  
### OpenEdge
Provider: EntityFrameworkCore.OpenEdge  
Version: 1.0.11  
ConnectionStringName: openedge  