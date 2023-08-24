using Lab_153503_Verhasau.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab_153503_Verhasau.Controllers {
    public class Home : Controller {
        public IActionResult Index() {
            ViewData["Lab"] = "Лабораторная работа №2";

            var listDemo = new List<ListDemo>
        {
            new ListDemo { Id = 1, Name = "Item 1" },
            new ListDemo { Id = 2, Name = "Item 2" },
            new ListDemo { Id = 3, Name = "Item 3" }
        };

            ViewData["ListDemo"] = listDemo;

            return View();
        }
    }
}
