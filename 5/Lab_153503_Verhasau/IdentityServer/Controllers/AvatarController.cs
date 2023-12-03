using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace IdentityServer.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class AvatarController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AvatarController> _logger;

    public AvatarController(IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager, ILogger<AvatarController> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvatar()
    {
        ApplicationUser user = await _userManager.GetUserAsync(User);
        FileExtensionContentTypeProvider contentTypeProvider = new();
        string contentType;
        if (user != null)
        {
            string imagePath = Directory.EnumerateFiles(Path.Combine(_webHostEnvironment.ContentRootPath, "images"), $"{user.Id}.*").First();
            contentTypeProvider.TryGetContentType(imagePath, out contentType);
            if (System.IO.File.Exists(imagePath))
            {
                _logger.LogDebug("User avatar was found");
                return PhysicalFile(imagePath, contentType);
            }
        }

        _logger.LogDebug("Default image used, user avatar was not found");
        string defaultFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "images", "default.jpeg");
        contentTypeProvider.TryGetContentType(defaultFilePath, out contentType);

        return PhysicalFile(defaultFilePath, contentType);
    }
}
