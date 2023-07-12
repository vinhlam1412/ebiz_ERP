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

namespace HQSOFT.Configuration.CSAttributes
{
    public class EfCoreCSAttributeRepository : EfCoreRepository<ConfigurationDbContext, CSAttribute, Guid>, ICSAttributeRepository
    {
        public EfCoreCSAttributeRepository(IDbContextProvider<ConfigurationDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<CSAttribute>> GetListAsync(
            string filterText = null,
            string attributeID = null,
            string description = null,
            ControlType? controlType = null,
            string entryMask = null,
            string regExp = null,
            string list = null,
            bool? isInternal = null,
            bool? containsPersonalData = null,
            string objectName = null,
            string fieldName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attributeID, description, controlType, entryMask, regExp, list, isInternal, containsPersonalData, objectName, fieldName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CSAttributeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string attributeID = null,
            string description = null,
            ControlType? controlType = null,
            string entryMask = null,
            string regExp = null,
            string list = null,
            bool? isInternal = null,
            bool? containsPersonalData = null,
            string objectName = null,
            string fieldName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, attributeID, description, controlType, entryMask, regExp, list, isInternal, containsPersonalData, objectName, fieldName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CSAttribute> ApplyFilter(
            IQueryable<CSAttribute> query,
            string filterText,
            string attributeID = null,
            string description = null,
            ControlType? controlType = null,
            string entryMask = null,
            string regExp = null,
            string list = null,
            bool? isInternal = null,
            bool? containsPersonalData = null,
            string objectName = null,
            string fieldName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttributeID.Contains(filterText) || e.Description.Contains(filterText) || e.EntryMask.Contains(filterText) || e.RegExp.Contains(filterText) || e.List.Contains(filterText) || e.ObjectName.Contains(filterText) || e.FieldName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attributeID), e => e.AttributeID.Contains(attributeID))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(controlType.HasValue, e => e.ControlType == controlType)
                    .WhereIf(!string.IsNullOrWhiteSpace(entryMask), e => e.EntryMask.Contains(entryMask))
                    .WhereIf(!string.IsNullOrWhiteSpace(regExp), e => e.RegExp.Contains(regExp))
                    .WhereIf(!string.IsNullOrWhiteSpace(list), e => e.List.Contains(list))
                    .WhereIf(isInternal.HasValue, e => e.IsInternal == isInternal)
                    .WhereIf(containsPersonalData.HasValue, e => e.ContainsPersonalData == containsPersonalData)
                    .WhereIf(!string.IsNullOrWhiteSpace(objectName), e => e.ObjectName.Contains(objectName))
                    .WhereIf(!string.IsNullOrWhiteSpace(fieldName), e => e.FieldName.Contains(fieldName));
        }
    }
}