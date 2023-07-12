using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HQSOFT.Configuration.Blazor.WebAssembly;

[DependsOn(
    typeof(ConfigurationBlazorModule),
    typeof(ConfigurationHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class ConfigurationBlazorWebAssemblyModule : AbpModule
{

}
