using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;

namespace Lab_153503_Verhasau.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ISouvenirService _service;

        public DeleteModel(ISouvenirService service)
        {
            _service = service;
        }

        [BindProperty]
        public Souvenir Souvenir { get; set; } = default!;

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
            } else
            {
                Souvenir = response.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _service.DeleteSouvenirAsync(id.Value);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
