using HQSOFT.Configuration.CSAttributeDetails;
using System;
using HQSOFT.Configuration.Shared;
using Volo.Abp.AutoMapper;
using HQSOFT.Configuration.CSAttributes;
using AutoMapper;

namespace HQSOFT.Configuration;

public class ConfigurationApplicationAutoMapperProfile : Profile
{
    public ConfigurationApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CSAttribute, CSAttributeDto>();
        CreateMap<CSAttribute, CSAttributeExcelDto>();

        CreateMap<CSAttributeDetail, CSAttributeDetailDto>();
        CreateMap<CSAttributeDetail, CSAttributeDetailExcelDto>();
        CreateMap<CSAttributeDetailWithNavigationProperties, CSAttributeDetailWithNavigationPropertiesDto>();
        CreateMap<CSAttribute, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttributeID));
    }
}