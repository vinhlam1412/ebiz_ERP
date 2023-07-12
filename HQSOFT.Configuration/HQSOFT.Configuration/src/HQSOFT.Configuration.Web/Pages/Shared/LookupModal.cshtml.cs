using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HQSOFT.Configuration.Web.Pages.Shared
{
    public class LookupModal : ConfigurationPageModel
    {
        public string CurrentId { get; set; }
        public string CurrentDisplayName { get; set; }

        public LookupModal()
        {
            CurrentId = string.Empty;
            CurrentDisplayName = string.Empty;
        }

        public Task OnGetAsync(string currentId, string currentDisplayName)
        {
            CurrentId = currentId;
            CurrentDisplayName = currentDisplayName;

            return Task.CompletedTask;
        }
    }
}