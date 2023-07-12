using HQSOFT.Configuration.CSAttributes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace HQSOFT.Configuration.CSAttributeDetails
{
    public class CSAttributeDetailWithNavigationPropertiesDto
    {
        public CSAttributeDetailDto CSAttributeDetail { get; set; }

        public CSAttributeDto CSAttribute { get; set; }

    }
}