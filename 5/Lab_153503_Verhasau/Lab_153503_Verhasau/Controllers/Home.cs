using Microsoft.AspNetCore.Mvc;

namespace Lab_153503_Verhasau.Controllers {
	public class Home : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}
