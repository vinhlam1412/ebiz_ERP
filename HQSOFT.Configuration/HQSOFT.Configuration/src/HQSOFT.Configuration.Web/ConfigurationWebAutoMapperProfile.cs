using HQSOFT.Configuration.Web.Pages.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.Web.Pages.Configuration.CSAttributes;
using Volo.Abp.AutoMapper;
using HQSOFT.Configuration.CSAttributes;
using AutoMapper;

namespace HQSOFT.Configuration.Web;

public class ConfigurationWebAutoMapperProfile : Profile
{
    public ConfigurationWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CSAttributeDto, CSAttributeUpdateViewModel>();
        CreateMap<CSAttributeUpdateViewModel, CSAttributeUpdateDto>();
        CreateMap<CSAttributeCreateViewModel, CSAttributeCreateDto>();

        CreateMap<CSAttributeDetailDto, CSAttributeDetailUpdateViewModel>();
        CreateMap<CSAttributeDetailUpdateViewModel, CSAttributeDetailUpdateDto>();
        CreateMap<CSAttributeDetailCreateViewModel, CSAttributeDetailCreateDto>();
    }
}