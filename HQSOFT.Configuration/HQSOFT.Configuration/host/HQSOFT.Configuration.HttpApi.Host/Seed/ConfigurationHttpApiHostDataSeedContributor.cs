using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.Configuration.Seed;

public class ConfigurationHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ConfigurationSampleDataSeeder _configurationSampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public ConfigurationHttpApiHostDataSeedContributor(
        ConfigurationSampleDataSeeder configurationSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _configurationSampleDataSeeder = configurationSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _configurationSampleDataSeeder.SeedAsync(context!);
        }
    }
}
