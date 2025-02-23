// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;




const string postgres = "postgres";
const string sqlserver = "sqlserver";



var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.development.json", true, true);

IConfiguration configuration = builder.Build();

await CreateDatabaseAsync(postgres);
await CreateDatabaseAsync(sqlserver);




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
        }
    
        var dbContextOptions = dbContextOptionsBuilder
            .Options;

        var context = new DbContext(dbContextOptions);
        if (await context.Database.EnsureCreatedAsync())
        {
            Console.WriteLine($"Database created for {connectionStringName}");
        }
        else
        {
            Console.WriteLine($"Database already exists for {connectionStringName}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Database could not be created for {connectionStringName}");
        Console.WriteLine(e);
    }
}