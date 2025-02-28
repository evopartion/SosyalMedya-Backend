using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentManager : ICommentServices
    {
        ICommentDal _commentDal;
        // ctrl + . generate ctor

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        public IResult Add(Comment entity)
        {
            _commentDal.Add(entity);
            return new SuccessResult(Messages.Comment_Add);
        }

        public IResult Delete(int id)
        {
            var deleteComment = _commentDal.Get(x => x.ID == id);
            _commentDal.Delete(deleteComment);
            return new SuccessResult(Messages.Comment_Delete);
        }

        public IDataResult<List<Comment>> GetAll()
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(),Messages.Comment_List);
        }

        public IDataResult<Comment> GetById(int id)
        {
            return new SuccessDataResult<Comment>(_commentDal.Get(x=>x.ID == id),Messages.Comment_Listed);
        }

        public IResult Update(Comment entity)
        {
            _commentDal.Update(entity);
            return new SuccessResult(Messages.Comment_Update);
        }
    }
}
