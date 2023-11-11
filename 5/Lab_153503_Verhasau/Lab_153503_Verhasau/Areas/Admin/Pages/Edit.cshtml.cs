using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;

namespace Lab_153503_Verhasau.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ISouvenirService _service;

        public EditModel(ISouvenirService service)
        {
            _service = service;
        }

        [BindProperty]
        public Souvenir Souvenir { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

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
