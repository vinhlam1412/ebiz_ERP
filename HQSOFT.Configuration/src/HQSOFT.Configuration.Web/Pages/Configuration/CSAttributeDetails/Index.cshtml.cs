using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using HQSOFT.Configuration.CSAttributeDetails;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.Web.Pages.Configuration.CSAttributeDetails
{
    public class IndexModel : AbpPageModel
    {
        public string? ValueIDFilter { get; set; }
        public string? DescriptionFilter { get; set; }
        public uint? SortOrderFilterMin { get; set; }

        public uint? SortOrderFilterMax { get; set; }
        [SelectItems(nameof(DisabledBoolFilterItems))]
        public string DisabledFilter { get; set; }

        public List<SelectListItem> DisabledBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(CSAttributeLookupList))]
        public Guid? CSAttributeIdFilter { get; set; }
        public List<SelectListItem> CSAttributeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly ICSAttributeDetailsAppService _cSAttributeDetailsAppService;

        public IndexModel(ICSAttributeDetailsAppService cSAttributeDetailsAppService)
        {
            _cSAttributeDetailsAppService = cSAttributeDetailsAppService;
        }

        public async Task OnGetAsync()
        {
            CSAttributeLookupList.AddRange((
                    await _cSAttributeDetailsAppService.GetCSAttributeLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}