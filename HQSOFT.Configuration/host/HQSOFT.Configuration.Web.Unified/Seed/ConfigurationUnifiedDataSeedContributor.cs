using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace HQSOFT.Configuration.Seed;

public class ConfigurationUnifiedDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ConfigurationSampleIdentityDataSeeder _sampleIdentityDataSeeder;
    private readonly ConfigurationSampleDataSeeder _configurationSampleDataSeeder;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ICurrentTenant _currentTenant;

    public ConfigurationUnifiedDataSeedContributor(
        ConfigurationSampleIdentityDataSeeder sampleIdentityDataSeeder,
        IUnitOfWorkManager unitOfWorkManager,
        ConfigurationSampleDataSeeder configurationSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _sampleIdentityDataSeeder = sampleIdentityDataSeeder;
        _unitOfWorkManager = unitOfWorkManager;
        _configurationSampleDataSeeder = configurationSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _unitOfWorkManager.Current.SaveChangesAsync();

        using (_currentTenant.Change(context?.TenantId))
        {
            await _sampleIdentityDataSeeder.SeedAsync(context);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            await _configurationSampleDataSeeder.SeedAsync(context);
        }
    }
}
