using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Presentation.Models;
using ArticleDetailDto = Web_Presentation.Models.ArticleDetailDto;

namespace Web_Presentation.ViewComponents.NotSeenComment
{
    public class _NotSeenComment:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _NotSeenComment(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {

            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            int userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId") ?? 0;

            var responseMessage = await _httpClientFactory.CreateClient().GetAsync("https://localhost:44339/api/Articles/getarticlewithdetailsbyuserid?id=" + userId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                var apiDataResponse = JsonConvert.DeserializeObject<ApiListDataResponse<ArticleDetailDto>>(jsonResponse);

                int notSeenComment = apiDataResponse.Data
                    .SelectMany(article => article.CommentDetails)
                    .Count(comment => comment.Status == false && comment.UserId!=userId);


                return View(notSeenComment);
            }
            return View();
        }
    }
}
