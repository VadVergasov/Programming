using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;

namespace Lab_153503_Verhasau.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ISouvenirService _service;

        public DetailsModel(ISouvenirService service)
        {
            _service = service;
        }

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
            }

            Souvenir = response.Data;
            return Page();
        }
    }
}
