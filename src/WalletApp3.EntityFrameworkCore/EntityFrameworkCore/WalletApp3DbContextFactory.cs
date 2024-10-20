using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WalletApp3.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class WalletApp3DbContextFactory : IDesignTimeDbContextFactory<WalletApp3DbContext>
{
    public WalletApp3DbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        WalletApp3EfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<WalletApp3DbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new WalletApp3DbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WalletApp3.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
