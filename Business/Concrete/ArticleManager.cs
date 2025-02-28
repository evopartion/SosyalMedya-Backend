using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConncerns.Logging.Log4Net.Logger;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArticleManager : IArticleServices
    {


        IArticleDal _articleDal;
        // dalı çağırdıktan sonra oto ctor
        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }
        // Miras verdikten sonra iplement et otomatik oluşur
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult Add(Article entity)
        {
            _articleDal.Add(entity);
            // addfield
            return new SuccessResult(Messages.Article_Add);
        }
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult Delete(int id)
        {
            var deleteArticle=_articleDal.Get(x=>x.ID == id);
            _articleDal.Delete(deleteArticle);
            return new SuccessResult(Messages.Article_Deleted);
        }
        // 10 dakika bellekte tut
        [CacheAspect(10)]
        public IDataResult<List<Article>> GetAll()
        {
            return new SuccessDataResult<List<Article>>(_articleDal.GetAll(), Messages.Articleses_Listed);
        }
        [CacheAspect(10)]
        public IDataResult<List<ArticleDetailDto>> GetArticleDetails()
        {
            return new SuccessDataResult<List<ArticleDetailDto>>(_articleDal.GetArticleDetails(), Messages.ArticleWithDetailListed);
        }
        [CacheAspect(10)]
        public IDataResult<Article> GetById(int id)
        {
            return new SuccessDataResult<Article>(_articleDal.Get(x => x.ID == id), Messages.Articleem_Listed);
        }
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        public IResult Update(Article entity)
        {
            _articleDal.Update(entity);
            return new SuccessResult(Messages.Article_Edit);
        }
    }
}
