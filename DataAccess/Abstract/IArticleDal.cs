using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;
using static Core.DataAccess.IEntityRepository;

namespace DataAccess.Abstract
{
    public interface IArticleDal : IEntityRepository<Article>
    {
            List<ArticleDetailDto> GetArticleDetails(Expression<Func<ArticleDetailDto, bool>> filter = null);
            ArticleDetailDto GetArticleDetailsById(Expression<Func<ArticleDetailDto, bool>> filter = null);
        
    }
}
