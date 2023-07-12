using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailManager : DomainService
    {
        private readonly ICSAttributeDetailRepository _cSAttributeDetailRepository;

        public CSAttributeDetailManager(ICSAttributeDetailRepository cSAttributeDetailRepository)
        {
            _cSAttributeDetailRepository = cSAttributeDetailRepository;
        }

        public async Task<CSAttributeDetail> CreateAsync(
        Guid? cSAttributeId, string valueID, string description, bool disabled, uint? sortOrder = null)
        {
            Check.NotNullOrWhiteSpace(valueID, nameof(valueID));
            Check.Length(valueID, nameof(valueID), CSAttributeDetailConsts.ValueIDMaxLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), CSAttributeDetailConsts.DescriptionMaxLength);

            var cSAttributeDetail = new CSAttributeDetail(
             GuidGenerator.Create(),
             cSAttributeId, valueID, description, disabled, sortOrder
             );

            return await _cSAttributeDetailRepository.InsertAsync(cSAttributeDetail);
        }

        public async Task<CSAttributeDetail> UpdateAsync(
            Guid id,
            Guid? cSAttributeId, string valueID, string description, bool disabled, uint? sortOrder = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(valueID, nameof(valueID));
            Check.Length(valueID, nameof(valueID), CSAttributeDetailConsts.ValueIDMaxLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), CSAttributeDetailConsts.DescriptionMaxLength);

            var cSAttributeDetail = await _cSAttributeDetailRepository.GetAsync(id);

            cSAttributeDetail.CSAttributeId = cSAttributeId;
            cSAttributeDetail.ValueID = valueID;
            cSAttributeDetail.Description = description;
            cSAttributeDetail.Disabled = disabled;
            cSAttributeDetail.SortOrder = sortOrder;

            cSAttributeDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _cSAttributeDetailRepository.UpdateAsync(cSAttributeDetail);
        }

    }
}