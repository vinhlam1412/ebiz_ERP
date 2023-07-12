using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HQSOFT.Configuration;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(ConfigurationDomainSharedModule)
)]
public class ConfigurationDomainModule : AbpModule
{

}
