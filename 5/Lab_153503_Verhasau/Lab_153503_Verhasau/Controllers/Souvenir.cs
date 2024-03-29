﻿using Lab_153503_Verhasau.Services.CategoryService;
using Lab_153503_Verhasau.Services.SouvenirService;
using Microsoft.AspNetCore.Mvc;
using Lab_153503_Verhasau.Extensions;

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

        [Route("Souvenir")]
        [Route("Souvenir/{category?}")]
        public async Task<IActionResult> Index(string? category, int pageNumber = 0)
		{
			var response = await _souvenirService.GetSouvenirListAsync(category, pageNumber);
			if (!response.Success)
			{
				return NotFound(response.ErrorMessage);
			}

			var allCategories = await _categoryService.GetCategoryAsync();
            if (!allCategories.Success)
            {
                return NotFound(allCategories.ErrorMessage);
            }

            ViewData["allCategories"] = allCategories.Data;
            var currentCategory = category == null ? "Все" : allCategories.Data!.FirstOrDefault(c => c.NormalizedName == category)?.Name;
            ViewData["currentCategory"] = currentCategory;
            ViewData["currentPage"] = response.Data!.CurrentPage;
            ViewData["totalPages"] = response.Data.TotalPages;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SouvenirCardsAndPagerPartial", new
                {
                    response.Data!.CurrentPage,
                    response.Data!.TotalPages,
					ReturnUrl=Request.Path + Request.QueryString.ToUriComponent(),
					Category=category,
                    Souvenirs = response.Data!.Items,
                    InAdminArea = false	
                });
            }

            return View(response.Data!.Items);
        }
	}
}
