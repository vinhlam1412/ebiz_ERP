using HQSOFT.Configuration.CSAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HQSOFT.Configuration.EntityFrameworkCore;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class EfCoreCSAttributeDetailRepository : EfCoreRepository<ConfigurationDbContext, CSAttributeDetail, Guid>, ICSAttributeDetailRepository
    {
        public EfCoreCSAttributeDetailRepository(IDbContextProvider<ConfigurationDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CSAttributeDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(cSAttributeDetail => new CSAttributeDetailWithNavigationProperties
                {
                    CSAttributeDetail = cSAttributeDetail,
                    CSAttribute = dbContext.Set<CSAttribute>().FirstOrDefault(c => c.Id == cSAttributeDetail.CSAttributeId)
                }).FirstOrDefault();
        }

        public async Task<List<CSAttributeDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null,
            Guid? cSAttributeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, valueID, description, sortOrderMin, sortOrderMax, disabled, cSAttributeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CSAttributeDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CSAttributeDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from cSAttributeDetail in (await GetDbSetAsync())
                   join cSAttribute in (await GetDbContextAsync()).Set<CSAttribute>() on cSAttributeDetail.CSAttributeId equals cSAttribute.Id into cSAttributes
                   from cSAttribute in cSAttributes.DefaultIfEmpty()
                   select new CSAttributeDetailWithNavigationProperties
                   {
                       CSAttributeDetail = cSAttributeDetail,
                       CSAttribute = cSAttribute
                   };
        }

        protected virtual IQueryable<CSAttributeDetailWithNavigationProperties> ApplyFilter(
            IQueryable<CSAttributeDetailWithNavigationProperties> query,
            string filterText,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null,
            Guid? cSAttributeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CSAttributeDetail.ValueID.Contains(filterText) || e.CSAttributeDetail.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueID), e => e.CSAttributeDetail.ValueID.Contains(valueID))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.CSAttributeDetail.Description.Contains(description))
                    .WhereIf(sortOrderMin.HasValue, e => e.CSAttributeDetail.SortOrder >= sortOrderMin.Value)
                    .WhereIf(sortOrderMax.HasValue, e => e.CSAttributeDetail.SortOrder <= sortOrderMax.Value)
                    .WhereIf(disabled.HasValue, e => e.CSAttributeDetail.Disabled == disabled)
                    .WhereIf(cSAttributeId != null && cSAttributeId != Guid.Empty, e => e.CSAttribute != null && e.CSAttribute.Id == cSAttributeId);
        }

        public async Task<List<CSAttributeDetail>> GetListAsync(
            string filterText = null,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, valueID, description, sortOrderMin, sortOrderMax, disabled);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CSAttributeDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null,
            Guid? cSAttributeId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, valueID, description, sortOrderMin, sortOrderMax, disabled, cSAttributeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CSAttributeDetail> ApplyFilter(
            IQueryable<CSAttributeDetail> query,
            string filterText,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ValueID.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueID), e => e.ValueID.Contains(valueID))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(sortOrderMin.HasValue, e => e.SortOrder >= sortOrderMin.Value)
                    .WhereIf(sortOrderMax.HasValue, e => e.SortOrder <= sortOrderMax.Value)
                    .WhereIf(disabled.HasValue, e => e.Disabled == disabled);
        }
    }
}