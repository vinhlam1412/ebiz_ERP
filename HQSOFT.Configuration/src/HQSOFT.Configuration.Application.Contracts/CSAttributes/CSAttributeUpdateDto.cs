using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttributeUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CSAttributeConsts.AttributeIDMaxLength)]
        public string AttributeID { get; set; }
        [Required]
        [StringLength(CSAttributeConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public ControlType ControlType { get; set; }
        [StringLength(CSAttributeConsts.EntryMaskMaxLength)]
        public string? EntryMask { get; set; }
        [StringLength(CSAttributeConsts.RegExpMaxLength)]
        public string? RegExp { get; set; }
        public string? List { get; set; }
        public bool IsInternal { get; set; }
        public bool ContainsPersonalData { get; set; }
        [StringLength(CSAttributeConsts.ObjectNameMaxLength)]
        public string? ObjectName { get; set; }
        [StringLength(CSAttributeConsts.FieldNameMaxLength)]
        public string? FieldName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}