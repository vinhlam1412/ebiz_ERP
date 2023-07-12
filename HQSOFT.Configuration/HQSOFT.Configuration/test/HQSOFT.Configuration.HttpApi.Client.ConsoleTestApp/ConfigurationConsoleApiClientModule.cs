using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HQSOFT.Configuration;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ConfigurationHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class ConfigurationConsoleApiClientModule : AbpModule
{

}
