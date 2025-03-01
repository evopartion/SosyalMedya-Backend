﻿using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;
using Web_Presentation.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfArticleDal : EfEntityRepository<Entities.Concrete.Article, SocialMediaContext>, IArticleDal
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
                                 UserId = A.UserID,
                                 Content = A.Content,
                                 UserName = U.FirstName + " " + U.LastName,
                                 SharingDate = A.SharingDate.ToShortDateString(),
                                 UserImage = ((from I in context.UserImages
                                               join user in context.Users on I.UserId equals user.ID
                                               where (A.UserID == I.UserId)
                                               select I.ImagePath)).First(),
                                 CommentDetails = ((from C in context.Comments
                                                    join User in context.Users on C.UserID equals User.ID
                                                    join uimg in context.UserImages on C.UserID equals uimg.UserId
                                                    where (A.ID == C.ArticleID)
                                                    select new CommentDetailDto
                                                    {
                                                        Id = C.ID,
                                                        ArticleId = C.ArticleID,
                                                        CommentText = C.CommentText,
                                                        UserId = C.UserID,
                                                        UserName = User.FirstName + " " + User.LastName,
                                                        Image = uimg.ImagePath,
                                                        CommentDate = C.CommentDate,
                                                        Status = C.Status
                                                    }).ToList()).Count == 0 ? new List<CommentDetailDto> { new CommentDetailDto { Id = -1, ArticleId = -1, CommentText = "Henüz yorum yapılmadı", CommentDate = DateTime.Now, UserId = -1, UserName = "", Image = "images/default.jpg" } }
                                            : (from C in context.Comments
                                               join User in context.Users on C.UserID equals User.ID
                                               join uimg in context.UserImages on C.UserID equals uimg.UserId
                                               where (A.ID == C.ArticleID)
                                               select new CommentDetailDto
                                               {
                                                   Id = C.ID,
                                                   ArticleId = C.ArticleID,
                                                   UserId = C.UserID,
                                                   UserName = User.FirstName + " " + User.LastName,
                                                   Image = uimg.ImagePath,
                                                   CommentText = C.CommentText,
                                                   
                                                   CommentDate = C.CommentDate,
                                                   Status = C.Status
                                               }).ToList()
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public ArticleDetailDto GetArticleDetailsById(Expression<Func<ArticleDetailDto, bool>> filter)
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
                                 UserId = A.UserID,
                                 Content = A.Content,
                                 UserName = U.FirstName + " " + U.LastName,
                                 SharingDate = A.SharingDate.ToShortDateString(),
                                 UserImage = ((from I in context.UserImages
                                               join user in context.Users on I.UserId equals user.ID
                                               where (A.UserID == I.UserId)
                                               select I.ImagePath)).First(),
                                 CommentDetails = ((from C in context.Comments
                                                    join User in context.Users on C.UserID equals User.ID
                                                    join uimg in context.UserImages on C.UserID equals uimg.UserId
                                                    where (A.ID == C.ArticleID)
                                                    select new CommentDetailDto
                                                    {
                                                        Id = C.ID,
                                                        ArticleId = C.ArticleID,
                                                        CommentText = C.CommentText,
                                                        UserId = C.UserID,
                                                        UserName = User.FirstName + " " + User.LastName,
                                                        Image = uimg.ImagePath,
                                                        CommentDate = C.CommentDate,
                                                        Status = C.Status
                                                    }).ToList()).Count == 0 ? new List<CommentDetailDto> { new CommentDetailDto { Id = -1, ArticleId = -1, CommentText = "Henüz yorum yapılmadı", CommentDate = DateTime.Now, UserId = -1, UserName = "", Image = "images/default.jpg" } }
                                            : (from C in context.Comments
                                               join User in context.Users on C.UserID equals User.ID
                                               join uimg in context.UserImages on C.UserID equals uimg.UserId
                                               where (A.ID == C.ArticleID)
                                               select new CommentDetailDto
                                               {
                                                   Id = C.ID,
                                                   ArticleId = C.ArticleID,
                                                   UserName = User.FirstName + " " + User.LastName,
                                                   Image = uimg.ImagePath,
                                                   CommentText = C.CommentText,
                                                   UserId = C.UserID,
                                                   CommentDate = C.CommentDate,
                                                   Status = C.Status
                                               }).ToList()
                             };

                return result.Where(filter).FirstOrDefault();
            }
        }
    }
}
