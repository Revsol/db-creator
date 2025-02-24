using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

const string postgres = "postgres";
const string sqlserver = "sqlserver";
const string mysql = "mysql";
const string oracle = "oracle";
const string mongodb = "mongodb";
const string interbase = "interbase";
const string firebird = "firebird";

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.development.json", true, true);
IConfiguration configuration = builder.Build();

await CreateDatabaseAsync(postgres);
await CreateDatabaseAsync(sqlserver);
await CreateDatabaseAsync(mysql);
await CreateDatabaseAsync(oracle);
await CreateDatabaseAsync(mongodb);
await CreateDatabaseAsync(interbase);
await CreateDatabaseAsync(firebird);

var keepRunning = configuration["KeepRunning"]?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;
while (keepRunning)
{
    await Task.Delay(1000);
}

async Task CreateDatabaseAsync(string connectionStringName)
{
    try
    {
        var connectionString = configuration.GetConnectionString(connectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine($"No connection string found for {connectionStringName}");
            return;
        }

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<DbContext>();
        switch (connectionStringName)
        {
            case postgres:
                dbContextOptionsBuilder.UseNpgsql(connectionString);
                break;
            case sqlserver:
                dbContextOptionsBuilder.UseSqlServer(connectionString);
                break;
            case mysql:
                dbContextOptionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                break;
            case oracle:
                dbContextOptionsBuilder.UseOracle(connectionString);
                break;
            case mongodb:
                dbContextOptionsBuilder.UseMongoDB(connectionString, "test_db");
                break;
            case interbase:
                dbContextOptionsBuilder.UseInterBase(connectionString);
                break;
            case firebird:
                dbContextOptionsBuilder.UseFirebird(connectionString);
                break;
        } ;

        var dbContextOptions = dbContextOptionsBuilder
            .Options;

        var context = new DbContext(dbContextOptions);
        if (await context.Database.EnsureCreatedAsync())
            Console.WriteLine($"Database created for {connectionStringName}");
        else
            Console.WriteLine($"Database already exists for {connectionStringName}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Database could not be created for {connectionStringName}");
        Console.WriteLine(e);
    }
}