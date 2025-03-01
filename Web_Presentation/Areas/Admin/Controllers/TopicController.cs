using Microsoft.AspNetCore.Mvc;

namespace Web_Presentation.Areas.Admin.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
