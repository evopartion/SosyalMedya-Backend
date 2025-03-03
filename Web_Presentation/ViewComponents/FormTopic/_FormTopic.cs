using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Presentation.Models;

namespace Web_Presentation.ViewComponents.FormTopic
{
    public class _FormTopic:ViewComponent
    {
            private readonly IHttpClientFactory _httpClientFactory;

            public _FormTopic(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var responseMessage = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44339/api/Topics/getall");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                    var apiDataResponse = JsonConvert.DeserializeObject<ApiListDataResponse<Topics>>(jsonResponse);
                    var trueTopic = apiDataResponse.Data.Where(x => x.Status == true).ToList();
                    return apiDataResponse.Success ? View(apiDataResponse.Data) : View();
                }
                return View();
            }
        }
}
