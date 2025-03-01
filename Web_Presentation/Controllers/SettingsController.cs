using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Web_Presentation.Models;

namespace Web_Presentation.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SettingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("hesap-bilgilerim")]
        public async Task<IActionResult> AccountSetting()

        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var responseMessage = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44339/api/Users/getbyid?id=" + userId);
            if (responseMessage.IsSuccessStatusCode)
            {
                //ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<UserDto>>(jsonResponse);
                //ViewData["MyArticle"] = HttpContext.Session.GetInt32("MyArticle");
                return apiDataResponse.Success ? View(apiDataResponse.Data) : (IActionResult)View("Veri gelmiyor");
            }
            return View("Veri gelmiyor");
        }

        [HttpPost("bilgileri-guncelle")]
        public async Task<IActionResult> UpdateAccountSetting(UserDto userDto)
        {
            //var httpClient = _httpClientFactory.CreateClient();
            //var token = HttpContext.Session.GetString("Token");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonUserDto = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(jsonUserDto, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClientFactory.CreateClient().PostAsync("https://localhost:44339/api/Users/update", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var successUpdatedUser = await GetUpdateUserResponseMessage(responseMessage);
                TempData["Message"] = successUpdatedUser.Message;
                TempData["Success"] = successUpdatedUser.Success;
                return RedirectToAction("AccountSetting", "Settings");
            }
            else
            {
                var successUpdatedUser = await GetUpdateUserResponseMessage(responseMessage);
                TempData["Message"] = successUpdatedUser.Message;
                TempData["Success"] = successUpdatedUser.Success;
                return View();
            }
        }

        [HttpPost("photo-update")]
        public async Task<IActionResult> UpdateUserImage(UserImage userImage)
        {
            if (userImage.ImagePath != null)
            {
                using (var formContent = new MultipartFormDataContent())
                {
                    formContent.Add(new StringContent(userImage.Id.ToString()), "Id");
                    formContent.Add(new StringContent(userImage.UserId.ToString()), "UserId");
                    formContent.Add(new StringContent(userImage.ImagePath.FileName), "ImagePath");
                    formContent.Add(new StreamContent(userImage.ImagePath.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength=userImage.ImagePath.Length,
                            ContentType=new MediaTypeHeaderValue(userImage.ImagePath.ContentType)
                        }
                    },
                    "ImageFile", userImage.ImagePath.FileName);

                    //var httpClient = _httpClientFactory.CreateClient();
                    //var token = HttpContext.Session.GetString("Token");
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var responseMesssage = await _httpClientFactory.CreateClient().PostAsync("https://localhost:44339/api/UserImages/update", formContent);
                    var successUpdatedUserImage = await GetUpdateUserImageResponseMessage(responseMesssage);
                    TempData["Message"] = successUpdatedUserImage.Message;
                    TempData["Success"] = successUpdatedUserImage.Success;

                    return RedirectToAction("AccountSetting", "Settings");
                }
            }
            return RedirectToAction("AccountSetting", "Settings");
        }

        
        [HttpGet("kod-dogrulama")]
        public async Task<IActionResult> GetVerifyCode()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
            ViewData["Email"] = HttpContext.Session.GetString("Email");
            return View();
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("kod")]
        public async Task<IActionResult> GetVerifyCode(VerificationCode verificationCodeDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonInfo = JsonConvert.SerializeObject(verificationCodeDto);
            var content = new StringContent(jsonInfo, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44339/api/VerificationCodes/sendcode", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = new
                {
                    Success = true,
                    Url = "kod-dogrulama"
                };

                return Json(response);
            }
            return RedirectToAction("AccountSetting", "Settings");
        }
        [Authorize(Roles = "admin,user")]
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode(VerificationCode verificationCodeDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonInfo = JsonConvert.SerializeObject(verificationCodeDto);
            var content = new StringContent(jsonInfo, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44339/api/VerificationCodes/checkcode", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<VerificationCode>>(responseContent);

                var response = new
                {
                    Success = true,
                    Message = apiDataResponse.Message,
                    Url = "sifre-guncelle"
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
        [Authorize(Roles = "admin,user")]
        [HttpGet("sifre-guncelle")]
        public async Task<IActionResult> ChangePassword()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            ViewData["Email"] = HttpContext.Session.GetString("Email");
            return View();
        }
        [Authorize(Roles = "admin,user")]
        [HttpPost("sifre-guncelle")]
        public async Task<IActionResult> ChangePassword(Models.ChangePassword changePassword)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("Token");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var jsonInfo = JsonConvert.SerializeObject(changePassword);
            var content = new StringContent(jsonInfo, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44339/api/Auth/changepassword", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<Models.ChangePassword>>(responseContent);

                var response = new
                {
                    Success = true,
                    Message = apiDataResponse.Message,
                    Url = "/"
                };

                return Json(response);
            }
            else
            {
                var response = new
                {
                    Message = "Şifre Güncellenemedi , lütfen tekrar deneyin",
                };
                return Json(response);
            }

        }

        private async Task<ApiDataResponse<UserImage>> GetUpdateUserImageResponseMessage(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiDataResponse<UserImage>>(responseContent);
        }
        private async Task<ApiDataResponse<UserDto>> GetUpdateUserResponseMessage(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiDataResponse<UserDto>>(responseContent);
        }
    }
}
