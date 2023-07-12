using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using HQSOFT.Configuration.Localization;
using HQSOFT.Configuration.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using HQSOFT.Configuration.Permissions;

namespace HQSOFT.Configuration.Web;

[DependsOn(
    typeof(ConfigurationApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class ConfigurationWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(ConfigurationResource), typeof(ConfigurationWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ConfigurationWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ConfigurationMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ConfigurationWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<ConfigurationWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ConfigurationWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/CSAttributes/Index", ConfigurationPermissions.CSAttributes.Default);
            options.Conventions.AuthorizePage("/CSAttributeDetails/Index", ConfigurationPermissions.CSAttributeDetails.Default);
        });
    }
}