using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttributeCreateDto
    {
        [Required]
        [StringLength(CSAttributeConsts.AttributeIDMaxLength)]
        public string AttributeID { get; set; }
        [Required]
        [StringLength(CSAttributeConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public ControlType ControlType { get; set; } = ((ControlType[])Enum.GetValues(typeof(ControlType)))[0];
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
    }
}