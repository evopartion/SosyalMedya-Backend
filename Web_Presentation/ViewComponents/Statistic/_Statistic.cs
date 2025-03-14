﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Presentation.Models;

namespace Web_Presentation.ViewComponents.Statistic
{
    public class _Statistic : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _Statistic(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseMessage = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44339/api/Articles/getall");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiListDataResponse<Article>>(jsonResponse);

                var responseMessageTopic = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44339/api/Topics/getall");
                if (responseMessageTopic.IsSuccessStatusCode)
                {
                    var jsonResponseTopic = await responseMessageTopic.Content.ReadAsStringAsync();
                    var apiDataResponseTopic = JsonConvert.DeserializeObject<ApiListDataResponse<Topics>>(jsonResponseTopic);

                    var responseMessageUser = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44339/api/Users/getall");
                    if (responseMessageTopic.IsSuccessStatusCode)
                    {
                        var jsonResponseUser = await responseMessageUser.Content.ReadAsStringAsync();
                        var apiDataResponseUser = JsonConvert.DeserializeObject<ApiListDataResponse<UserDto>>(jsonResponseUser);

                        var tuple = (apiDataResponse.Data.Count(), apiDataResponseTopic.Data.Count(), apiDataResponseUser.Data.Count());
                        return View(tuple);
                    }
                }

            }
            return View();
        }
    }
}
