using HQSOFT.Configuration.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HQSOFT.Configuration.CSAttributeDetails;

namespace HQSOFT.Configuration.Web.Pages.Configuration.CSAttributeDetails
{
    public class CreateModalModel : ConfigurationPageModel
    {
        [BindProperty]
        public CSAttributeDetailCreateViewModel CSAttributeDetail { get; set; }

        public List<SelectListItem> CSAttributeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICSAttributeDetailsAppService _cSAttributeDetailsAppService;

        public CreateModalModel(ICSAttributeDetailsAppService cSAttributeDetailsAppService)
        {
            _cSAttributeDetailsAppService = cSAttributeDetailsAppService;

            CSAttributeDetail = new();
        }

        public async Task OnGetAsync()
        {
            CSAttributeDetail = new CSAttributeDetailCreateViewModel();
            CSAttributeLookupList.AddRange((
                                    await _cSAttributeDetailsAppService.GetCSAttributeLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _cSAttributeDetailsAppService.CreateAsync(ObjectMapper.Map<CSAttributeDetailCreateViewModel, CSAttributeDetailCreateDto>(CSAttributeDetail));
            return NoContent();
        }
    }

    public class CSAttributeDetailCreateViewModel : CSAttributeDetailCreateDto
    {
    }
}