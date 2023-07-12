using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HQSOFT.Configuration.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class ConfigurationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Configuration";
}
