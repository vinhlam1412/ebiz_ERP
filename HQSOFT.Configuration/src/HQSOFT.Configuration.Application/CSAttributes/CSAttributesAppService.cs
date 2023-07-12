using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HQSOFT.Configuration.Permissions;
using HQSOFT.Configuration.CSAttributes;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.CSAttributes
{

    [Authorize(ConfigurationPermissions.CSAttributes.Default)]
    public class CSAttributesAppService : ApplicationService, ICSAttributesAppService
    {
        private readonly IDistributedCache<CSAttributeExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICSAttributeRepository _cSAttributeRepository;
        private readonly CSAttributeManager _cSAttributeManager;

        public CSAttributesAppService(ICSAttributeRepository cSAttributeRepository, CSAttributeManager cSAttributeManager, IDistributedCache<CSAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _cSAttributeRepository = cSAttributeRepository;
            _cSAttributeManager = cSAttributeManager;
        }

        public virtual async Task<PagedResultDto<CSAttributeDto>> GetListAsync(GetCSAttributesInput input)
        {
            var totalCount = await _cSAttributeRepository.GetCountAsync(input.FilterText, input.AttributeID, input.Description, input.ControlType, input.EntryMask, input.RegExp, input.List, input.IsInternal, input.ContainsPersonalData, input.ObjectName, input.FieldName);
            var items = await _cSAttributeRepository.GetListAsync(input.FilterText, input.AttributeID, input.Description, input.ControlType, input.EntryMask, input.RegExp, input.List, input.IsInternal, input.ContainsPersonalData, input.ObjectName, input.FieldName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CSAttributeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CSAttribute>, List<CSAttributeDto>>(items)
            };
        }

        public virtual async Task<CSAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CSAttribute, CSAttributeDto>(await _cSAttributeRepository.GetAsync(id));
        }

        [Authorize(ConfigurationPermissions.CSAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _cSAttributeRepository.DeleteAsync(id);
        }

        [Authorize(ConfigurationPermissions.CSAttributes.Create)]
        public virtual async Task<CSAttributeDto> CreateAsync(CSAttributeCreateDto input)
        {

            var cSAttribute = await _cSAttributeManager.CreateAsync(
            input.AttributeID, input.Description, input.ControlType, input.EntryMask, input.RegExp, input.List, input.IsInternal, input.ContainsPersonalData, input.ObjectName, input.FieldName
            );

            return ObjectMapper.Map<CSAttribute, CSAttributeDto>(cSAttribute);
        }

        [Authorize(ConfigurationPermissions.CSAttributes.Edit)]
        public virtual async Task<CSAttributeDto> UpdateAsync(Guid id, CSAttributeUpdateDto input)
        {

            var cSAttribute = await _cSAttributeManager.UpdateAsync(
            id,
            input.AttributeID, input.Description, input.ControlType, input.EntryMask, input.RegExp, input.List, input.IsInternal, input.ContainsPersonalData, input.ObjectName, input.FieldName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CSAttribute, CSAttributeDto>(cSAttribute);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CSAttributeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _cSAttributeRepository.GetListAsync(input.FilterText, input.AttributeID, input.Description, input.ControlType, input.EntryMask, input.RegExp, input.List, input.IsInternal, input.ContainsPersonalData, input.ObjectName, input.FieldName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CSAttribute>, List<CSAttributeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CSAttributes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CSAttributeExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}