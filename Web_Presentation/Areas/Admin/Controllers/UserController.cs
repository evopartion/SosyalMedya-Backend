using Microsoft.AspNetCore.Mvc;

namespace Web_Presentation.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
