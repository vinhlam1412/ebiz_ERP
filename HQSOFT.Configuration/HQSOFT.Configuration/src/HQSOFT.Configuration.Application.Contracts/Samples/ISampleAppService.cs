using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HQSOFT.Configuration.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
