using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfArticleDal : EfEntityRepository<Article, SocialMediaContext>, IArticleDal
    {
        public List<ArticleDetailDto> GetArticleDetails(Expression<Func<ArticleDetailDto, bool>> filter = null)
        {
            using (var context = new SocialMediaContext())
            {
                var result = from A in context.Articles
                             join T in context.Topics on A.TopicID equals T.ID
                             join U in context.Users on A.UserID equals U.ID
                             select new ArticleDetailDto
                             {
                                 Id = A.ID,
                                 TopicId = A.TopicID,
                                 TopicTitle = T.TopicTitle,
                                 Content=A.Content,
                                 UserId = A.UserID,
                                 SharingDate=A.SharingDate.ToShortDateString(),
                                 UserName = U.FirstName + " " + U.LastName,
 
                                 CommentDetails = ((from C in context.Comments
                                                    join User in context.Users on C.UserID equals User.ID
                                                    where (A.ID == C.ArticleID)
                                                    select new CommentDetailDto
                                                    {
                                                        Id = C.ID,
                                                        ArticleId = C.ArticleID,
                                                        CommentText = C.CommentText,
                                                        UserId = C.UserID,
                                                        UserName = User.FirstName + " " + User.LastName,
                                                        CommentDate = C.CommentDate,
                                                        Status= C.Status,
                                                    }).ToList()).Count == 0 ? new List<CommentDetailDto> { new CommentDetailDto { Id = -1, ArticleId = -1, CommentText = "Henüz yorum yapılmadı", CommentDate = DateTime.Now, UserId = -1, UserName = "" } }
                                            : (from C in context.Comments
                                               join User in context.Users on C.UserID equals User.ID
                                               where (A.ID == C.ArticleID)
                                               select new CommentDetailDto
                                               {
                                                   Id = C.ID,
                                                   ArticleId = C.ArticleID,
                                                   UserName = User.FirstName + " " + User.LastName,
                                                   CommentText = C.CommentText,
                                                   UserId = C.UserID,
                                                   CommentDate = C.CommentDate,
                                                   Status = C.Status,
                                               }).ToList()
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
