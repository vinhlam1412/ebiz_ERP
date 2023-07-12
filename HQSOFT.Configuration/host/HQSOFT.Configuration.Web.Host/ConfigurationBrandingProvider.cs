using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HQSOFT.Configuration;

[Dependency(ReplaceServices = true)]
public class ConfigurationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Configuration";
}
