﻿using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Presentation.Models;

namespace Web_Presentation.Controllers
{
    public class ProfileController : Controller
    {
        IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Authorize(Roles = "admin,user")]
        [HttpGet("profilim")]
        public async Task<IActionResult> Profile()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            ViewData["MyArticle"] = HttpContext.Session.GetInt32("MyArticle");
            ViewData["UserId"] = UserId;


            var httpClient = _httpClientFactory.CreateClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44339/api/Articles/getarticlewithdetailsbyuserid?id=" + UserId);
            // isteğin başarılı olup olmadığını kontrol etmek için
            if (responseMessage.IsSuccessStatusCode)
            {
                //ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiListDataResponse<ArticleDetailDto>>(jsonResponse);
                return apiDataResponse.Success ? View(apiDataResponse.Data) : (IActionResult)View("Veri gelmiyor");
            }
            return View();
        }
    }
}
