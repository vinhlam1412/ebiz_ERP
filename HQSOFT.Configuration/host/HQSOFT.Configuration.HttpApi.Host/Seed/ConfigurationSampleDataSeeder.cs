using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace HQSOFT.Configuration.Seed;

/* You can use this file to seed some sample data
 * to test your module easier.
 *
 * This class is shared among these projects:
 * - HQSOFT.Configuration.AuthServer
 * - HQSOFT.Configuration.Web.Unified (used as linked file)
 */
public class ConfigurationSampleDataSeeder : ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {

    }
}
