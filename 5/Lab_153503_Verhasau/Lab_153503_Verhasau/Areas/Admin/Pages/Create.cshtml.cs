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

        public CreateModel(ISouvenirService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _categoryService.GetCategoryAsync();
            if (!response.Success)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(response.Data, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Souvenir Souvenir { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _service.CreateSouvenirAsync(Souvenir);

            if (!response.Success)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
