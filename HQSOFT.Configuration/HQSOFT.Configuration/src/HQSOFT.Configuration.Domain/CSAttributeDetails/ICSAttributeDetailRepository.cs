using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public interface ICSAttributeDetailRepository : IRepository<CSAttributeDetail, Guid>
    {
        Task<CSAttributeDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CSAttributeDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<CSAttributeDetail>> GetListAsync(
                    string filterText = null,
                    string valueID = null,
                    string description = null,
                    uint? sortOrderMin = null,
                    uint? sortOrderMax = null,
                    bool? disabled = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string valueID = null,
            string description = null,
            uint? sortOrderMin = null,
            uint? sortOrderMax = null,
            bool? disabled = null,
            Guid? cSAttributeId = null,
            CancellationToken cancellationToken = default);
    }
}