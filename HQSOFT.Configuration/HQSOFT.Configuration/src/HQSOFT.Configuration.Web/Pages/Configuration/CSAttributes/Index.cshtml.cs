using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using HQSOFT.Configuration.CSAttributes;
using HQSOFT.Configuration.Shared;

namespace HQSOFT.Configuration.Web.Pages.Configuration.CSAttributes
{
    public class IndexModel : AbpPageModel
    {
        public string? AttributeIDFilter { get; set; }
        public string? DescriptionFilter { get; set; }
        public ControlType? ControlTypeFilter { get; set; }
        public string? EntryMaskFilter { get; set; }
        public string? RegExpFilter { get; set; }
        public string? ListFilter { get; set; }
        [SelectItems(nameof(IsInternalBoolFilterItems))]
        public string IsInternalFilter { get; set; }

        public List<SelectListItem> IsInternalBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ContainsPersonalDataBoolFilterItems))]
        public string ContainsPersonalDataFilter { get; set; }

        public List<SelectListItem> ContainsPersonalDataBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public string? ObjectNameFilter { get; set; }
        public string? FieldNameFilter { get; set; }

        private readonly ICSAttributesAppService _cSAttributesAppService;

        public IndexModel(ICSAttributesAppService cSAttributesAppService)
        {
            _cSAttributesAppService = cSAttributesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}