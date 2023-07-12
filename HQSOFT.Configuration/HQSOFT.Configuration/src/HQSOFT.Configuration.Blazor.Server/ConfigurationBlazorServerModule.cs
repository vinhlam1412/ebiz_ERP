using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace HQSOFT.Configuration.Blazor.Server;

[DependsOn(
    typeof(ConfigurationBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class ConfigurationBlazorServerModule : AbpModule
{

}
