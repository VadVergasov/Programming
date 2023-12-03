using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities;
using Lab_153503_Verhasau.Services.SouvenirService;

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

        public IList<Souvenir> Souvenir { get;set; } = default!;

        public async Task OnGetAsync(int pageNo = 0)
        {
            var response = await _service.GetSouvenirListAsync(null, pageNo);

            if (response.Success)
            {
                Souvenir = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
