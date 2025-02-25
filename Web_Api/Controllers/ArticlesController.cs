using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Result.Abstract.IResult;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleServices _articleService;

        public ArticlesController(IArticleServices articleService) => _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));

        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            IDataResult<List<Article>> result = _articleService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("getbyid")]
        public ActionResult GetById(int id)
        {
            IDataResult<Article> result = _articleService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("add")]
        public ActionResult Add(Article article)
        {
            IResult result = _articleService.Add(article);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Article article)
        {
            IResult result = _articleService.Update(article);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public ActionResult Delete(Article article)
        {
            IResult result = _articleService.Delete(article);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
