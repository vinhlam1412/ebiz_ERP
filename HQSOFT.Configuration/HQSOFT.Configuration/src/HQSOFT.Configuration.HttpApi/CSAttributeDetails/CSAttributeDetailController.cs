using HQSOFT.Configuration.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.Configuration.CSAttributeDetails;
using Volo.Abp.Content;
using HQSOFT.Configuration.Shared;
using System.Collections.Generic;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    [RemoteService(Name = "Configuration")]
    [Area("configuration")]
    [ControllerName("CSAttributeDetail")]
    [Route("api/configuration/c-sAttribute-details")]
    public class CSAttributeDetailController : AbpController, ICSAttributeDetailsAppService
    {
        private readonly ICSAttributeDetailsAppService _cSAttributeDetailsAppService;

        public CSAttributeDetailController(ICSAttributeDetailsAppService cSAttributeDetailsAppService)
        {
            _cSAttributeDetailsAppService = cSAttributeDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto>> GetListAsync(GetCSAttributeDetailsInput input)
        {
            return _cSAttributeDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CSAttributeDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _cSAttributeDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CSAttributeDetailDto> GetAsync(Guid id)
        {
            return _cSAttributeDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("c-sAttribute-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCSAttributeLookupAsync(LookupRequestDto input)
        {
            return _cSAttributeDetailsAppService.GetCSAttributeLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CSAttributeDetailDto> CreateAsync(CSAttributeDetailCreateDto input)
        {
            return _cSAttributeDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CSAttributeDetailDto> UpdateAsync(Guid id, CSAttributeDetailUpdateDto input)
        {
            return _cSAttributeDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _cSAttributeDetailsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CSAttributeDetailExcelDownloadDto input)
        {
            return _cSAttributeDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _cSAttributeDetailsAppService.GetDownloadTokenAsync();
        }

        [HttpGet]
        [Route("list-detail/{id}")]
        public Task<List<CSAttributeDetailDto>> GetListAllAttriDetail(Guid id)
        {
            return _cSAttributeDetailsAppService.GetListAllAttriDetail(id);
        }
    }
}