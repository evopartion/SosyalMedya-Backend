using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_Presentation.Models;

namespace Web_Presentation.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("giris-yap")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("LoginPost")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var httpClient=new HttpClient();
            var jsonLogin=JsonConvert.SerializeObject(userForLogin);
            StringContent content = new StringContent(jsonLogin,Encoding.UTF8,"application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44339/api/Auth/Login", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                var responseLogin=JsonConvert.DeserializeObject<ApiAuthDataResponse<UserForLogin>>(responseContent);
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}
