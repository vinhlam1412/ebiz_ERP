using HQSOFT.Configuration.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using HQSOFT.Configuration.CSAttributes;

namespace HQSOFT.Configuration.Web.Pages.Configuration.CSAttributes
{
    public class EditModalModel : ConfigurationPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CSAttributeUpdateViewModel CSAttribute { get; set; }

        private readonly ICSAttributesAppService _cSAttributesAppService;

        public EditModalModel(ICSAttributesAppService cSAttributesAppService)
        {
            _cSAttributesAppService = cSAttributesAppService;

            CSAttribute = new();
        }

        public async Task OnGetAsync()
        {
            var cSAttribute = await _cSAttributesAppService.GetAsync(Id);
            CSAttribute = ObjectMapper.Map<CSAttributeDto, CSAttributeUpdateViewModel>(cSAttribute);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _cSAttributesAppService.UpdateAsync(Id, ObjectMapper.Map<CSAttributeUpdateViewModel, CSAttributeUpdateDto>(CSAttribute));
            return NoContent();
        }
    }

    public class CSAttributeUpdateViewModel : CSAttributeUpdateDto
    {
    }
}