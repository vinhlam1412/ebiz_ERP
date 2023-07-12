using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? ValueID { get; set; }
        public string? Description { get; set; }
        public uint? SortOrderMin { get; set; }
        public uint? SortOrderMax { get; set; }
        public bool? Disabled { get; set; }
        public Guid? CSAttributeId { get; set; }

        public CSAttributeDetailExcelDownloadDto()
        {

        }
    }
}