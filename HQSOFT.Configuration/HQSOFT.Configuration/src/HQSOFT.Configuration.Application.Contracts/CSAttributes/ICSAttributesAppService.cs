using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.CSAttributes
{
    public interface ICSAttributesAppService : IApplicationService
    {
        Task<PagedResultDto<CSAttributeDto>> GetListAsync(GetCSAttributesInput input);

        Task<CSAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CSAttributeDto> CreateAsync(CSAttributeCreateDto input);

        Task<CSAttributeDto> UpdateAsync(Guid id, CSAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CSAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}