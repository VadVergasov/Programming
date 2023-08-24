using Microsoft.AspNetCore.Mvc;

namespace Lab_153503_Verhasau.ViewComponents {
    public class Cart : ViewComponent {
        public Task<IViewComponentResult> InvokeAsync() {
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}
