using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace HQSOFT.Configuration;

[DependsOn(
    typeof(ConfigurationDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class ConfigurationApplicationContractsModule : AbpModule
{

}
