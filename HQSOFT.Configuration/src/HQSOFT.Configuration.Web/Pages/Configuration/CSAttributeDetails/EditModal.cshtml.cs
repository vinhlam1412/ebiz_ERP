using HQSOFT.Configuration.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using HQSOFT.Configuration.CSAttributeDetails;

namespace HQSOFT.Configuration.Web.Pages.Configuration.CSAttributeDetails
{
    public class EditModalModel : ConfigurationPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CSAttributeDetailUpdateViewModel CSAttributeDetail { get; set; }

        public List<SelectListItem> CSAttributeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICSAttributeDetailsAppService _cSAttributeDetailsAppService;

        public EditModalModel(ICSAttributeDetailsAppService cSAttributeDetailsAppService)
        {
            _cSAttributeDetailsAppService = cSAttributeDetailsAppService;

            CSAttributeDetail = new();
        }

        public async Task OnGetAsync()
        {
            var cSAttributeDetailWithNavigationPropertiesDto = await _cSAttributeDetailsAppService.GetWithNavigationPropertiesAsync(Id);
            CSAttributeDetail = ObjectMapper.Map<CSAttributeDetailDto, CSAttributeDetailUpdateViewModel>(cSAttributeDetailWithNavigationPropertiesDto.CSAttributeDetail);

            CSAttributeLookupList.AddRange((
                                    await _cSAttributeDetailsAppService.GetCSAttributeLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _cSAttributeDetailsAppService.UpdateAsync(Id, ObjectMapper.Map<CSAttributeDetailUpdateViewModel, CSAttributeDetailUpdateDto>(CSAttributeDetail));
            return NoContent();
        }
    }

    public class CSAttributeDetailUpdateViewModel : CSAttributeDetailUpdateDto
    {
    }
}