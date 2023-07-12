using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.Configuration.CSAttributes
{
    public interface ICSAttributeRepository : IRepository<CSAttribute, Guid>
    {
        Task<List<CSAttribute>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}