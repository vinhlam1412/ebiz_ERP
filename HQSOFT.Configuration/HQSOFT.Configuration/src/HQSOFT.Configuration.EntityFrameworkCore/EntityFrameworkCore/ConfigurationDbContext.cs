using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.CSAttributes;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HQSOFT.Configuration.EntityFrameworkCore;

[ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
public class ConfigurationDbContext : AbpDbContext<ConfigurationDbContext>, IConfigurationDbContext
{
    public DbSet<CSAttributeDetail> CSAttributeDetails { get; set; }
    public DbSet<CSAttribute> CSAttributes { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureConfiguration();
    }
}