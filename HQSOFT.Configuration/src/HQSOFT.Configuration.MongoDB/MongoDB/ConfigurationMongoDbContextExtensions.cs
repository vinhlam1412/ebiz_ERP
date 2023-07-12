using Volo.Abp;
using Volo.Abp.MongoDB;

namespace HQSOFT.Configuration.MongoDB;

public static class ConfigurationMongoDbContextExtensions
{
    public static void ConfigureConfiguration(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
