using Volo.Abp.Reflection;

namespace HQSOFT.Configuration.Permissions;

public class ConfigurationPermissions
{
    public const string GroupName = "Configuration";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ConfigurationPermissions));
    }

    public static class CSAttributes
    {
        public const string Default = GroupName + ".CSAttributes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class CSAttributeDetails
    {
        public const string Default = GroupName + ".CSAttributeDetails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}