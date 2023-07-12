using HQSOFT.Configuration.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HQSOFT.Configuration.Permissions;

public class ConfigurationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ConfigurationPermissions.GroupName, L("Permission:Configuration"));

        var cSAttributePermission = myGroup.AddPermission(ConfigurationPermissions.CSAttributes.Default, L("Permission:CSAttributes"));
        cSAttributePermission.AddChild(ConfigurationPermissions.CSAttributes.Create, L("Permission:Create"));
        cSAttributePermission.AddChild(ConfigurationPermissions.CSAttributes.Edit, L("Permission:Edit"));
        cSAttributePermission.AddChild(ConfigurationPermissions.CSAttributes.Delete, L("Permission:Delete"));

        var cSAttributeDetailPermission = myGroup.AddPermission(ConfigurationPermissions.CSAttributeDetails.Default, L("Permission:CSAttributeDetails"));
        cSAttributeDetailPermission.AddChild(ConfigurationPermissions.CSAttributeDetails.Create, L("Permission:Create"));
        cSAttributeDetailPermission.AddChild(ConfigurationPermissions.CSAttributeDetails.Edit, L("Permission:Edit"));
        cSAttributeDetailPermission.AddChild(ConfigurationPermissions.CSAttributeDetails.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ConfigurationResource>(name);
    }
}