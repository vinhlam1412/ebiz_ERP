using HQSOFT.Configuration.CSAttributeDetails;
using Volo.Abp.AutoMapper;
using HQSOFT.Configuration.CSAttributes;
using AutoMapper;

namespace HQSOFT.Configuration.Blazor;

public class ConfigurationBlazorAutoMapperProfile : Profile
{
    public ConfigurationBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CSAttributeDto, CSAttributeUpdateDto>();
        CreateMap<CSAttributeDto, CSAttributeCreateDto>();
        CreateMap<CSAttributeUpdateDto, CSAttributeCreateDto>();

        CreateMap<CSAttributeDetailDto, CSAttributeDetailUpdateDto>();
        CreateMap<CSAttributeDetailDto, CSAttributeDetailCreateDto>();
    }
}