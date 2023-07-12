using HQSOFT.Configuration.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HQSOFT.Configuration.CSAttributes;

namespace HQSOFT.Configuration.Web.Pages.Configuration.CSAttributes
{
    public class CreateModalModel : ConfigurationPageModel
    {
        [BindProperty]
        public CSAttributeCreateViewModel CSAttribute { get; set; }

        private readonly ICSAttributesAppService _cSAttributesAppService;

        public CreateModalModel(ICSAttributesAppService cSAttributesAppService)
        {
            _cSAttributesAppService = cSAttributesAppService;

            CSAttribute = new();
        }

        public async Task OnGetAsync()
        {
            CSAttribute = new CSAttributeCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _cSAttributesAppService.CreateAsync(ObjectMapper.Map<CSAttributeCreateViewModel, CSAttributeCreateDto>(CSAttribute));
            return NoContent();
        }
    }

    public class CSAttributeCreateViewModel : CSAttributeCreateDto
    {
    }
}