using System;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailExcelDto
    {
        public string ValueID { get; set; }
        public string Description { get; set; }
        public uint? SortOrder { get; set; }
        public bool Disabled { get; set; }
    }
}