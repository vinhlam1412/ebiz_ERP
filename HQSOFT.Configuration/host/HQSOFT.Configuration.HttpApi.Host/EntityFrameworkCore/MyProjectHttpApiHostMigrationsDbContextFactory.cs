using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HQSOFT.Configuration.EntityFrameworkCore;

public class ConfigurationHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<ConfigurationHttpApiHostMigrationsDbContext>
{
    public ConfigurationHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ConfigurationHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Configuration"));

        return new ConfigurationHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
