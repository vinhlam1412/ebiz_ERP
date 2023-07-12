using HQSOFT.Configuration.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HQSOFT.Configuration.Pages;

public abstract class ConfigurationPageModel : AbpPageModel
{
    protected ConfigurationPageModel()
    {
        LocalizationResourceType = typeof(ConfigurationResource);
    }
}
