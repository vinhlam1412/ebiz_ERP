using HQSOFT.Configuration.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using HQSOFT.Configuration.Localization;
using Volo.Abp.UI.Navigation;

namespace HQSOFT.Configuration.Blazor.Menus;

public class ConfigurationMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemCSAttributes(context, moduleMenu);

        AddMenuItemCSAttributeDetails(context, moduleMenu);
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<ConfigurationResource>();

        context.Menu.AddItem(new ApplicationMenuItem(ConfigurationMenus.Prefix, displayName: "Sample Page", "/Configuration", icon: "fa fa-globe"));

        await Task.CompletedTask;
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            ConfigurationMenus.Prefix,
            context.GetLocalizer<ConfigurationResource>()["Menu:Configuration"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemCSAttributes(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.ConfigurationMenus.CSAttributes,
                context.GetLocalizer<ConfigurationResource>()["Menu:CSAttributes"],
                "/Configuration/CSAttributes",
                icon: "fa fa-file-alt",
                requiredPermissionName: ConfigurationPermissions.CSAttributes.Default
            )
        );
    }

    private static void AddMenuItemCSAttributeDetails(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.ConfigurationMenus.CSAttributeDetails,
                context.GetLocalizer<ConfigurationResource>()["Menu:CSAttributeDetails"],
                "/Configuration/CSAttributeDetails",
                icon: "fa fa-file-alt",
                requiredPermissionName: ConfigurationPermissions.CSAttributeDetails.Default
            )
        );
    }
}