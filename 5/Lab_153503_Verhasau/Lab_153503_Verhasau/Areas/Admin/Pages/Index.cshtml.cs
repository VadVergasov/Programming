using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;
using Microsoft.AspNetCore.Mvc;
using Lab_153503_Verhasau.Extensions;

namespace Lab_153503_Verhasau.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISouvenirService _service;

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public IndexModel(ISouvenirService service)
        {
            _service = service;
        }

        public IList<Souvenir> Souvenirs { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int pageNo = 0)
        {
            var response = await _service.GetSouvenirListAsync(null, pageNo);

            if (response.Success)
            {
                Souvenirs = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }

            if (Request.IsAjaxRequest())
			{
				return Partial("_SouvenirCardsAndPagerPartial", new
				{
					CurrentPage,
					TotalPages,
					Souvenirs,
					InAdminArea = true,
				});
			}

			return Page();
        }
    }
}
