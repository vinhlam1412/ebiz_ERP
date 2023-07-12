using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.CSAttributes;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace HQSOFT.Configuration.MongoDB;

[ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
public interface IConfigurationMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<CSAttributeDetail> CSAttributeDetails { get; }
    IMongoCollection<CSAttribute> CSAttributes { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}