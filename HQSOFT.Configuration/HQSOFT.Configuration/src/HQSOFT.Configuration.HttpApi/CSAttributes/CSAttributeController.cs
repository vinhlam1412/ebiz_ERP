using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.Configuration.CSAttributes;
using Volo.Abp.Content;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.CSAttributes
{
    [RemoteService(Name = "Configuration")]
    [Area("configuration")]
    [ControllerName("CSAttribute")]
    [Route("api/configuration/c-sAttributes")]
    public class CSAttributeController : AbpController, ICSAttributesAppService
    {
        private readonly ICSAttributesAppService _cSAttributesAppService;

        public CSAttributeController(ICSAttributesAppService cSAttributesAppService)
        {
            _cSAttributesAppService = cSAttributesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CSAttributeDto>> GetListAsync(GetCSAttributesInput input)
        {
            return _cSAttributesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CSAttributeDto> GetAsync(Guid id)
        {
            return _cSAttributesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CSAttributeDto> CreateAsync(CSAttributeCreateDto input)
        {
            return _cSAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CSAttributeDto> UpdateAsync(Guid id, CSAttributeUpdateDto input)
        {
            return _cSAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _cSAttributesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CSAttributeExcelDownloadDto input)
        {
            return _cSAttributesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _cSAttributesAppService.GetDownloadTokenAsync();
        }
    }
}