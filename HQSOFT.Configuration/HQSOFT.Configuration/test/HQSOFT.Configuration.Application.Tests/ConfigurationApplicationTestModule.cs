using Volo.Abp.Modularity;

namespace HQSOFT.Configuration;

[DependsOn(
    typeof(ConfigurationApplicationModule),
    typeof(ConfigurationDomainTestModule)
    )]
public class ConfigurationApplicationTestModule : AbpModule
{

}
