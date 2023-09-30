using Domain.Entities;
using Lab_153503_Verhasau.Services.CategoryService;
using Lab_153503_Verhasau.Services.SouvenirService;
using Microsoft.AspNetCore.Mvc;

namespace Lab_153503_Verhasau.Controllers
{
	public class Souvenir : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly ISouvenirService _souvenirService;
		private readonly int _pageSize;

		public Souvenir([FromServices] IConfiguration config, ISouvenirService souvenirService, ICategoryService categoryService)
		{
			_categoryService = categoryService;
			_souvenirService = souvenirService;
			_pageSize = Convert.ToInt32(config["ItemsPerPage"]);
		}
		public async Task<IActionResult> Index(string? category, int pageNumber = 0)
		{
			var categories = await _categoryService.GetCategoryAsync();
			var response = await _souvenirService.GetSouvenirListAsync(category);
			if (!response.Success)
			{
				return NotFound(response.ErrorMessage);
			}
			return View(
				(
					categories.Data.AsEnumerable(),
					response.Data.Items.Skip(_pageSize * pageNumber).Take(_pageSize).AsEnumerable(),
					categories.Data.FirstOrDefault(x => x.NormalizedName == category) ?? new Category { Name = "Все" },
					Convert.ToInt32(Math.Ceiling(Convert.ToDouble(response.Data.Items.Count()) / Convert.ToDouble(_pageSize)))
				)
			);
		}
	}
}
