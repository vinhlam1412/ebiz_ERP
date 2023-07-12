using HQSOFT.Configuration.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HQSOFT.Configuration.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ConfigurationPageModel : AbpPageModel
{
    protected ConfigurationPageModel()
    {
        LocalizationResourceType = typeof(ConfigurationResource);
        ObjectMapperContext = typeof(ConfigurationWebModule);
    }
}
