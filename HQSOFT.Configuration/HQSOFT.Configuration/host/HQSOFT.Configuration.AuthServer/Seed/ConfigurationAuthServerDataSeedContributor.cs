using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.Configuration.Seed;

public class ConfigurationAuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ConfigurationSampleIdentityDataSeeder _configurationSampleIdentityDataSeeder;
    private readonly ConfigurationAuthServerDataSeeder _configurationAuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public ConfigurationAuthServerDataSeedContributor(
        ConfigurationAuthServerDataSeeder configurationAuthServerDataSeeder,
        ConfigurationSampleIdentityDataSeeder configurationSampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _configurationAuthServerDataSeeder = configurationAuthServerDataSeeder;
        _configurationSampleIdentityDataSeeder = configurationSampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _configurationSampleIdentityDataSeeder.SeedAsync(context!);
            await _configurationAuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
