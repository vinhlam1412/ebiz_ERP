using HQSOFT.Configuration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HQSOFT.Configuration;

public abstract class ConfigurationController : AbpControllerBase
{
    protected ConfigurationController()
    {
        LocalizationResource = typeof(ConfigurationResource);
    }
}
