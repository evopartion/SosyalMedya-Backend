using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Presentation.Models;

namespace Web_Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
