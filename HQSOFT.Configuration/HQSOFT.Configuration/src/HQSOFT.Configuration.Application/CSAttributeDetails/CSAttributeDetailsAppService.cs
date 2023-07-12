using HQSOFT.Configuration.Shared;
using HQSOFT.Configuration.CSAttributes;
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
using HQSOFT.Configuration.CSAttributeDetails;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.CSAttributeDetails
{

    [Authorize(ConfigurationPermissions.CSAttributeDetails.Default)]
    public class CSAttributeDetailsAppService : ApplicationService, ICSAttributeDetailsAppService
    {
        private readonly IDistributedCache<CSAttributeDetailExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICSAttributeDetailRepository _cSAttributeDetailRepository;
        private readonly CSAttributeDetailManager _cSAttributeDetailManager;
        private readonly IRepository<CSAttribute, Guid> _cSAttributeRepository;

        public CSAttributeDetailsAppService(ICSAttributeDetailRepository cSAttributeDetailRepository, CSAttributeDetailManager cSAttributeDetailManager, IDistributedCache<CSAttributeDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<CSAttribute, Guid> cSAttributeRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _cSAttributeDetailRepository = cSAttributeDetailRepository;
            _cSAttributeDetailManager = cSAttributeDetailManager; _cSAttributeRepository = cSAttributeRepository;
        }

        public virtual async Task<PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto>> GetListAsync(GetCSAttributeDetailsInput input)
        {
            var totalCount = await _cSAttributeDetailRepository.GetCountAsync(input.FilterText, input.ValueID, input.Description, input.SortOrderMin, input.SortOrderMax, input.Disabled, input.CSAttributeId);
            var items = await _cSAttributeDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ValueID, input.Description, input.SortOrderMin, input.SortOrderMax, input.Disabled, input.CSAttributeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CSAttributeDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CSAttributeDetailWithNavigationProperties>, List<CSAttributeDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CSAttributeDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CSAttributeDetailWithNavigationProperties, CSAttributeDetailWithNavigationPropertiesDto>
                (await _cSAttributeDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CSAttributeDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CSAttributeDetail, CSAttributeDetailDto>(await _cSAttributeDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCSAttributeLookupAsync(LookupRequestDto input)
        {
            var query = (await _cSAttributeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttributeID != null &&
                         x.AttributeID.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CSAttribute>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CSAttribute>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ConfigurationPermissions.CSAttributeDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _cSAttributeDetailRepository.DeleteAsync(id);
        }

        [Authorize(ConfigurationPermissions.CSAttributeDetails.Create)]
        public virtual async Task<CSAttributeDetailDto> CreateAsync(CSAttributeDetailCreateDto input)
        {

            var cSAttributeDetail = await _cSAttributeDetailManager.CreateAsync(
            input.CSAttributeId, input.ValueID, input.Description, input.Disabled, input.SortOrder
            );

            return ObjectMapper.Map<CSAttributeDetail, CSAttributeDetailDto>(cSAttributeDetail);
        }

        [Authorize(ConfigurationPermissions.CSAttributeDetails.Edit)]
        public virtual async Task<CSAttributeDetailDto> UpdateAsync(Guid id, CSAttributeDetailUpdateDto input)
        {

            var cSAttributeDetail = await _cSAttributeDetailManager.UpdateAsync(
            id,
            input.CSAttributeId, input.ValueID, input.Description, input.Disabled, input.SortOrder, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CSAttributeDetail, CSAttributeDetailDto>(cSAttributeDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CSAttributeDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var cSAttributeDetails = await _cSAttributeDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ValueID, input.Description, input.SortOrderMin, input.SortOrderMax, input.Disabled);
            var items = cSAttributeDetails.Select(item => new
            {
                ValueID = item.CSAttributeDetail.ValueID,
                Description = item.CSAttributeDetail.Description,
                SortOrder = item.CSAttributeDetail.SortOrder,
                Disabled = item.CSAttributeDetail.Disabled,

                CSAttribute = item.CSAttribute?.AttributeID,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CSAttributeDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CSAttributeDetailExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public async Task<List<CSAttributeDetailDto>> GetListAllAttriDetail(Guid id)
        {
            var attributeDetails = await _cSAttributeDetailRepository.GetListAsync(x => x.CSAttributeId == id);
            return ObjectMapper.Map<List<CSAttributeDetail>, List<CSAttributeDetailDto>>(attributeDetails);
        }
    }
}