using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Presentation.Models;

namespace Web_Presentation.Areas.Admin.ViewComponents.GetClaimComponent.cs
{
    public class _GetClaimComponent: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _GetClaimComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseMessage = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44347/api/OperationClaims/getall");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiListDataResponse<OperationClaim>>(jsonResponse);

                return View(apiDataResponse.Data);

            }
            return View();
        }
    }
}
