using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CSAttributeDetailConsts.ValueIDMaxLength)]
        public string ValueID { get; set; }
        [Required]
        [StringLength(CSAttributeDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public uint? SortOrder { get; set; }
        public bool Disabled { get; set; }
        public Guid? CSAttributeId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}