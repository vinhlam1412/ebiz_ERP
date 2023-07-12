using System;

namespace HQSOFT.Configuration.CSAttributes
{
    public class CSAttributeExcelDto
    {
        public string AttributeID { get; set; }
        public string Description { get; set; }
        public ControlType ControlType { get; set; }
        public string? EntryMask { get; set; }
        public string? RegExp { get; set; }
        public string? List { get; set; }
        public bool IsInternal { get; set; }
        public bool ContainsPersonalData { get; set; }
        public string? ObjectName { get; set; }
        public string? FieldName { get; set; }
    }
}