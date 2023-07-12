using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HQSOFT.Configuration;

[DependsOn(
    typeof(ConfigurationApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ConfigurationHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(ConfigurationApplicationContractsModule).Assembly,
            ConfigurationRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ConfigurationHttpApiClientModule>();
        });
    }
}
