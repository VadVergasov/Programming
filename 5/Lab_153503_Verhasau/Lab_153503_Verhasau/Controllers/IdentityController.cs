using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WEB_153505_Vlasenko.Controllers;
public class IdentityController : Controller
{
    private readonly ILogger<IdentityController> _logger;

    public IdentityController(ILogger<IdentityController> logger)
    {
        _logger = logger;
    }

    public async Task Login()
    {
        await HttpContext.ChallengeAsync("oidc", new AuthenticationProperties()
        {
            RedirectUri = Url.Action("Index", "Home")
        });
    }

    [HttpPost]
    public async Task Logout()
    {
        _logger.LogDebug("Logout beginning");
        await HttpContext.SignOutAsync("cookie");
        _logger.LogDebug("Sign out cookie");
        await HttpContext.SignOutAsync("oidc",
        new AuthenticationProperties
        {
            RedirectUri = Url.Action("Index", "Home")
        });
        _logger.LogDebug("Sign out oidc");
    }
}
