using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.CSAttributes;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HQSOFT.Configuration.EntityFrameworkCore;

[DependsOn(
    typeof(ConfigurationDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class ConfigurationEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ConfigurationDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<CSAttribute, CSAttributes.EfCoreCSAttributeRepository>();

            options.AddRepository<CSAttributeDetail, CSAttributeDetails.EfCoreCSAttributeDetailRepository>();

        });
    }
}