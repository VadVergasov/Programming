using Lab_153503_Verhasau.Services.CategoryService;
using Lab_153503_Verhasau.Services.SouvenirService;
using Microsoft.AspNetCore.Mvc;

namespace Lab_153503_Verhasau.Controllers
{
    public class Souvenir : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISouvenirService _souvenirService;

        public Souvenir(ISouvenirService souvenirService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _souvenirService = souvenirService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _souvenirService.GetSouvenirListAsync(_categoryService.GetCategoryAsync().Result.Data.ToList().First().NormalizedName);
            if (!response.Success)
            {
                return NotFound(response.ErrorMessage);
            }
            return View(response.Data.Items);
        }
    }
}
