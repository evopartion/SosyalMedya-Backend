using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_Presentation.Models;
using Web_Presentation.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Web_Presentation.Controllers
{
    public class AuthController : Controller
    {
        private const string AdminRole = "admin";
        private const string UserRole = "user";
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactor)
        {
            _httpClientFactory = httpClientFactor;
        }

        [HttpGet("giris-yap")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("LoginPost")]
        public async Task<IActionResult> LoginPost(UserForLogin userForLogin)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var jsonLogin = JsonConvert.SerializeObject(userForLogin);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44339/api/Auth/Login", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var userForLoginSuccess = await GetUserForLogin(responseMessage);
                TempData["Message"]=userForLoginSuccess.Message;
                TempData["Success"]=userForLoginSuccess.Success;
                var jwtToken = userForLoginSuccess.Data.Token;
                var roleClaims = ExtractRoleClaimsFromJwtToken.GetRoleClaims(jwtToken);
                var userId = ExtractUserIdentityFromJwtToken.GetUserIdentityFromJwtToken(jwtToken);

                HttpContext.Session.SetInt32("UserId", userId);
                return await SignInUserByRole(roleClaims);
            }
            else
            {
                var userForLoginError = await GetUserForLogin(responseMessage);
                TempData["LoginFail"]=userForLoginError.Message;
                return RedirectToAction("Login", "Auth");
            }
            
        }
        private async Task<IActionResult> SignInUserByRole(List<string> roleClaims)
        {
            if (roleClaims != null && roleClaims.Any())
            {
                if (roleClaims.Contains(AdminRole))
                {
                    return await SignInUserByRoleClaim(AdminRole);
                }
                if (roleClaims.Contains(UserRole))
                {
                    return await SignInUserByRoleClaim(UserRole);
                }
            }
            return RedirectToAction("Login", "Home");
        }

        private async Task<IActionResult> SignInUserByRoleClaim(string role)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var userIdentity = new ClaimsIdentity(claims, role);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index", "Home");
        }

        private async Task<ApiDataResponse<UserForLogin>> GetUserForLogin(HttpResponseMessage responseMessage)
        {
            string responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiDataResponse<UserForLogin>>(responseContent);

        }
    }
}
