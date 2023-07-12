using HQSOFT.Configuration.CSAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using HQSOFT.Configuration.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class MongoCSAttributeDetailRepository : MongoDbRepository<ConfigurationMongoDbContext, CSAttributeDetail, Guid>, ICSAttributeDetailRepository
    {
        public MongoCSAttributeDetailRepository(IMongoDbContextProvider<ConfigurationMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<CSAttributeDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cSAttributeDetail = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var cSAttribute = await (await GetDbContextAsync(cancellationToken)).Collection<CSAttribute>().AsQueryable().FirstOrDefaultAsync(e => e.Id == cSAttributeDetail.CSAttributeId, cancellationToken: cancellationToken);

            return new CSAttributeDetailWithNavigationProperties
            {
                CSAttributeDetail = cSAttributeDetail,
                CSAttribute = cSAttribute,

            };
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, valueID, description, sortOrderMin, sortOrderMax, disabled, cSAttributeId);
            var cSAttributeDetails = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CSAttributeDetailConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<CSAttributeDetail>>()
                .PageBy<CSAttributeDetail, IMongoQueryable<CSAttributeDetail>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return cSAttributeDetails.Select(s => new CSAttributeDetailWithNavigationProperties
            {
                CSAttributeDetail = s,
                CSAttribute = dbContext.Collection<CSAttribute>().AsQueryable().FirstOrDefault(e => e.Id == s.CSAttributeId),

            }).ToList();
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, valueID, description, sortOrderMin, sortOrderMax, disabled);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CSAttributeDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<CSAttributeDetail>>()
                .PageBy<CSAttributeDetail, IMongoQueryable<CSAttributeDetail>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, valueID, description, sortOrderMin, sortOrderMax, disabled, cSAttributeId);
            return await query.As<IMongoQueryable<CSAttributeDetail>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CSAttributeDetail> ApplyFilter(
            IQueryable<CSAttributeDetail> query,
            string filterText,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null,
            Guid? cSAttributeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ValueID.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueID), e => e.ValueID.Contains(valueID))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(sortOrderMin.HasValue, e => e.SortOrder >= sortOrderMin.Value)
                    .WhereIf(sortOrderMax.HasValue, e => e.SortOrder <= sortOrderMax.Value)
                    .WhereIf(disabled.HasValue, e => e.Disabled == disabled)
                    .WhereIf(cSAttributeId != null && cSAttributeId != Guid.Empty, e => e.CSAttributeId == cSAttributeId);
        }
    }
}