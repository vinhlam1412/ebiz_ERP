using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace HQSOFT.Configuration.Pages;

public class IndexModel : ConfigurationPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
