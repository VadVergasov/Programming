using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab_153503_Verhasau.Services.SouvenirService;

namespace Lab_153503_Verhasau.Controllers;
public class Cart : Controller
{
    private readonly ISouvenirService _souvenirService;
    private readonly Domain.Models.Cart _sessionCart;

    public Cart(ISouvenirService clothesService, Domain.Models.Cart sessionCart)
    {
        _souvenirService = clothesService;
        _sessionCart = sessionCart;
    }

    public IActionResult Index()
    {
        return View(_sessionCart);
    }

    [Authorize]
    [Route("[controller]/add/{id:int}")]
    public async Task<ActionResult> Add(int id, string returnUrl)
    {
        var data = await _souvenirService.GetSouvenirByIdAsync(id);
        if (data.Success)
        {
            _sessionCart.Add(data.Data!);
        }
        return Redirect(returnUrl);
    }

    [Authorize]
    [Route("[controller]/remove/{id:int}")]
    public async Task<ActionResult> Remove(int id, string returnUrl)
    {
        var data = await _souvenirService.GetSouvenirByIdAsync(id);
        if (data.Success)
        {
            _sessionCart.Remove(data.Data!.Id);
        }
        return Redirect(returnUrl);
    }

}
