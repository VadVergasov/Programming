using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;
using Lab_153503_Verhasau.Services.CategoryService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_153503_Verhasau.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ISouvenirService _service;
        private readonly ICategoryService _categoryService;
        public readonly SelectList Categories;

        public EditModel(ISouvenirService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
            Categories = new SelectList(categoryService.GetCategoryAsync().Result.Data, nameof(Category.Id), nameof(Category.Name));
        }

        [BindProperty]
        public Souvenir Souvenir { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _service.GetSouvenirByIdAsync(id.Value);
            if (!response.Success)
            {
                return NotFound();
            }
            Souvenir = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Souvenir.Category = _categoryService.GetCategoryAsync().Result.Data.Single(x => x.Id == CategoryId);
            try
            {
                await _service.UpdateSouvenirAsync(Souvenir.Id, Souvenir, Image);
            } catch (Exception)
            {
                if (!await SouvenirExists(Souvenir.Id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> SouvenirExists(int id)
        {
            var response = await _service.GetSouvenirByIdAsync(id);
            return response.Success;
        }
    }
}
