using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Web_Presentation.Models;

namespace Web_Presentation.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpClient=new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44339/api/Articles/getarticlewithdetails");
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiListDataResponse<ArticleDetailDto>>(jsonResponse);

                int myArticle = apiDataResponse.Data.Count(x => x.UserId == Convert.ToInt32(ViewData["UserId"]));
                HttpContext.Session.SetInt32("MyArticle", myArticle);
                ViewData["MyArticle"] = myArticle;

                return apiDataResponse.Success ? View(apiDataResponse.Data) : (IActionResult)View("veri gelmiyor");
            }
            return View("Veri gelmiyor");
        }
    }
}
