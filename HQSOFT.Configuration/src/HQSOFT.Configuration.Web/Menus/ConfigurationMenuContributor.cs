using HQSOFT.Configuration.Permissions;
using HQSOFT.Configuration.Localization;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;

namespace HQSOFT.Configuration.Web.Menus;

public class ConfigurationMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!

        AddMenuItemCSAttributes(context, moduleMenu);

        AddMenuItemCSAttributeDetails(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            ConfigurationMenus.Prefix,
            displayName: "Configuration",
            "~/Configuration",
            icon: "fa fa-globe");

        //Add main menu items.
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