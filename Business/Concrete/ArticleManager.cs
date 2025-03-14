﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
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
        private readonly IArticleDal _articleDal;
        private readonly ICommentDal _commentDal;

        public ArticleManager(IArticleDal articleDal, ICommentDal commentDal)
        {
            _articleDal = articleDal;
            _commentDal = commentDal;
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ArticleValidator))]
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        public IResult Add(Article entity)
        {
            _articleDal.Add(entity);
            return new SuccessResult(Messages.ArticleAdded);
        }

        [LogAspect(typeof(FileLogger))]
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        public IResult Delete(int id)
        {
            var deletedArticle = _articleDal.Get(x => x.Id == id);
            if (deletedArticle != null)
            {
                var deletedComment = _commentDal.GetAll(x => x.ArticleId == deletedArticle.Id);
                if (deletedComment != null)
                {
                    foreach (var item in deletedComment)
                    {
                        _commentDal.Delete(item);
                    }
                }
                _articleDal.Delete(deletedArticle);
                return new SuccessResult(Messages.ArticleDeleted);
            }
            return new ErrorResult(Messages.ArticleNotFound);
        }

        [CacheAspect(1)]
        public IDataResult<List<Article>> GetAll()
        {
            return new SuccessDataResult<List<Article>>(_articleDal.GetAll(), Messages.ArticlesListed);
        }

         [CacheAspect(10)]
        public IDataResult<List<ArticleDetailDto>> GetArticleDetails()
        {
            return new SuccessDataResult<List<ArticleDetailDto>>(_articleDal.GetArticleDetails(), Messages.ArticleWithDetailListed);
        }

        public IDataResult<ArticleDetailDto> GetArticleDetailsById(int id)
        {
            return new SuccessDataResult<ArticleDetailDto>(_articleDal.GetArticleDetailsById(x => x.Id == id), Messages.ArticleWithDetailListed);
        }

        public IDataResult<List<ArticleDetailDto>> GetArticleDetailsByUserId(int id)
        {
            return new SuccessDataResult<List<ArticleDetailDto>>(_articleDal.GetArticleDetails(x => x.UserId == id), Messages.ArticleWithDetailListed);
        }

        [CacheAspect(10)]
        public IDataResult<Article> GetEntityById(int id)
        {
            return new SuccessDataResult<Article>(_articleDal.Get(x => x.Id == id), Messages.ArticleListed);
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ArticleValidator))]
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IArticleService.Get")]
        public IResult Update(Article entity)
        {
            _articleDal.Update(entity);
            return new SuccessResult(Messages.ArticleUpdated);
        }

    }
}
