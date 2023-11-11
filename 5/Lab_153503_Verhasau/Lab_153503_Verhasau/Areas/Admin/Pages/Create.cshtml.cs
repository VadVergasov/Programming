using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab_153503_Verhasau.Services.CategoryService;

namespace Lab_153503_Verhasau.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ISouvenirService _service;
        private readonly ICategoryService _categoryService;
        public readonly SelectList Categories;

        public CreateModel(ISouvenirService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
            Categories = new SelectList(categoryService.GetCategoryAsync().Result.Data, nameof(Category.Id), nameof(Category.Name));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _categoryService.GetCategoryAsync();
            if (!response.Success)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public Souvenir Souvenir { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Souvenir.Category = _categoryService.GetCategoryAsync().Result.Data.Single(x => x.Id == CategoryId);

            var response = await _service.CreateSouvenirAsync(Souvenir, Image);

            if (!response.Success)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
