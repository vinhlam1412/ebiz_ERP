using HQSOFT.Configuration.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public interface ICSAttributeDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto>> GetListAsync(GetCSAttributeDetailsInput input);

        Task<CSAttributeDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CSAttributeDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCSAttributeLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CSAttributeDetailDto> CreateAsync(CSAttributeDetailCreateDto input);

        Task<CSAttributeDetailDto> UpdateAsync(Guid id, CSAttributeDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CSAttributeDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<List<CSAttributeDetailDto>> GetListAllAttriDetail(Guid id);
    }
}