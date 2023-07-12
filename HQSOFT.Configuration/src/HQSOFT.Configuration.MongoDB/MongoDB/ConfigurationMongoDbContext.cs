using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.CSAttributes;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace HQSOFT.Configuration.MongoDB;

[ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
public class ConfigurationMongoDbContext : AbpMongoDbContext, IConfigurationMongoDbContext
{
    public IMongoCollection<CSAttributeDetail> CSAttributeDetails => Collection<CSAttributeDetail>();
    public IMongoCollection<CSAttribute> CSAttributes => Collection<CSAttribute>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureConfiguration();

        modelBuilder.Entity<CSAttribute>(b => { b.CollectionName = ConfigurationDbProperties.DbTablePrefix + "CSAttributes"; });

        modelBuilder.Entity<CSAttributeDetail>(b => { b.CollectionName = ConfigurationDbProperties.DbTablePrefix + "CSAttributeDetails"; });
    }
}