﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCommentleDal : EfEntityRepositoryBase<Comment, SocialMediaContext>, ICommentDal
    {
        //public List<CommentDetail> NotSeenComment(Expression<Func<CommentDetail, bool>> filter = null)
        //{
        //    using (var context = new SocialMediaContext())
        //    {
        //        var result = from A in context.Articles
        //                     join C in context.Comments on A.Id equals C.ArticleId
        //                     select new CommentDetail
        //                     {
        //                         Id = C.Id,
        //                         ArticleId
        //                     };

        //    }
        //}
    }
}
