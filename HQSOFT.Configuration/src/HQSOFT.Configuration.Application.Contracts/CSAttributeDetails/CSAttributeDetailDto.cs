using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ValueID { get; set; }
        public string Description { get; set; }
        public uint? SortOrder { get; set; }
        public bool Disabled { get; set; }
        public Guid? CSAttributeId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}