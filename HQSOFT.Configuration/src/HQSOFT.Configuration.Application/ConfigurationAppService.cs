using HQSOFT.Configuration.Localization;
using Volo.Abp.Application.Services;

namespace HQSOFT.Configuration;

public abstract class ConfigurationAppService : ApplicationService
{
    protected ConfigurationAppService()
    {
        LocalizationResource = typeof(ConfigurationResource);
        ObjectMapperContext = typeof(ConfigurationApplicationModule);
    }
}
