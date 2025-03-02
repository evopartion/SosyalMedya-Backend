﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_Presentation.Models;
using Web_Presentation.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using UserForRegisterDto = Web_Presentation.Models.UserForRegisterDto;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using NuGet.Common;

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Web;


namespace Web_Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string userName;
        private object ExtractUserNameFromJwtToken;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("giris-yap")]
        public IActionResult Login() => View();

        [HttpPost("LoginPost")]
        public async Task<IActionResult> LoginPost(Entities.DTOs.UserForLoginDto loginDto)
        {
            var jsonLoginDto = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(jsonLoginDto, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClientFactory.CreateClient().PostAsync("https://localhost:44339/api/Auth/login", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var userForLogin = await GetUserForLogin(responseMessage);

                TempData["Baslik"] = "Giriş Başarılı";
                TempData["Message"] = " Merhaba " + userForLogin.Message + ", hoş geldin.";
                TempData["Success"] = userForLogin.Success;

                if (userForLogin.Data != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(userForLogin.Data.Token);
                    var claims = token.Claims.ToList();

                    if (userForLogin.Data.Token != null)
                    {
                        _httpClientFactory.CreateClient().DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userForLogin.Data.Token);
                        var userId = ExtractUserIdentityFromJwtToken.GetUserIdentityFromJwtToken(userForLogin.Data.Token);
                        //var userName = ExtractUserNameFromJwtToken.GetUserNameFromJwtToken(userForLogin.Data.Token);

                        HttpContext.Session.SetInt32("UserId", userId);
                        HttpContext.Session.SetString("Email", loginDto.Email);
                        HttpContext.Session.SetString("UserName", userName);
                        HttpContext.Session.SetString("Token", userForLogin.Data.Token);

                        claims.Add(new Claim("socialmediawebsitetoken", userForLogin.Data.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = userForLogin.Data.Expiration,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var userForLogin = await GetUserForLogin(responseMessage);
                TempData["LoginFail"] = userForLogin.Message;
                return RedirectToAction("Login", "Auth");
            }
        }

        private async Task<ApiDataResponse<Entities.DTOs.UserForLoginDto>> GetUserForLogin(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiDataResponse<Entities.DTOs.UserForLoginDto>>(responseContent);
        }

        [HttpGet("aramiza-katil")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto registerDto)
        {
            var jsonRegisterDto = JsonConvert.SerializeObject(registerDto);
            var content = new StringContent(jsonRegisterDto, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClientFactory.CreateClient().PostAsync("https://localhost:44339/api/Auth/register", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = new
                {
                    Success = true,
                    Message = "Başarılı bir şekilde kayıt oldunuz. Giriş sayfasına yönlendiriliyorsunuz",
                    Url = "/giris-yap"
                };
                return Json(response);
            }
            else
            {
                var response = new
                {
                    Success = false,
                    Message = "Bilgilerinizi kontrol edip tekrar deneyin"
                };
                return Json(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResetPassword resetPassword)
        {
            var jsonEmail = JsonConvert.SerializeObject(resetPassword);
            var contentEmail = new StringContent(jsonEmail, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClientFactory.CreateClient().PostAsync("https://localhost:44339/api/VerificationCodes/sendcodeforpasswordreset", contentEmail);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = new
                {
                    Success = true,
                    Message = "E-posta adresinize bir doğrulama kodu gönderildi",
                    Url = "/Auth/CheckCode"
                };
                TempData["UserEmail"] = resetPassword.Email;
                return Json(response);
            }
            else
            {
                var response = new
                {
                    Success = false,
                    Message = "Bilgilerinizi kontrol edip tekrar deneyin"
                };
                return Json(response);
            }
        }

        [HttpGet]
        public IActionResult CheckCode()
        {
            ViewData["Email"] = TempData["UserEmail"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckCode(ResetPassword resetPassword)
        {
            var jsonInfo = JsonConvert.SerializeObject(resetPassword);
            var content = new StringContent(jsonInfo, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClientFactory.CreateClient().PostAsync($"https://localhost:44339/api/VerificationCodes/checkcodeforpasswordreset", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<Entities.Concrete.VerificationCode>>(jsonResponse);
                TempData["UserEmail"] = resetPassword.Email;
                var response = new
                {
                    Success = apiDataResponse.Success,
                    Message = apiDataResponse.Message,
                    Url = "/Auth/ResetPassword"
                };
                return Json(response);

            }
            else
            {
                var response = new
                {
                    Message = "Kod doğrulanamadı ! . Lütfen tekrar deneyin",
                };
                return Json(response);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewData["Email"] = TempData["UserEmail"];
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword(Models.ChangePassword changePassword)
        {
            var jsonData = JsonConvert.SerializeObject(changePassword);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClientFactory.CreateClient().PutAsync($"https://localhost:44339/api/Auth/adminchangepassword", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<Models.ChangePassword>>(jsonResponse);

                var response = new
                {
                    Success = apiDataResponse.Success,
                    Message = apiDataResponse.Message,
                    Url = "/giris-yap"

                };
                return Json(response);
            }
            else
            {
                var response = new
                {
                    Success = false,
                    Message = "Şifre güncellenemedi, lütfen tekrar deneyin",
                };
                return Json(response);
            }
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
