using HQSOFT.Configuration.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HQSOFT.Configuration.Blazor;

public abstract class ConfigurationComponentBase : AbpComponentBase
{
    protected ConfigurationComponentBase()
    {
        LocalizationResource = typeof(ConfigurationResource);
    }
}
