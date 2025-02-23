using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public IResult Add(Article entity)
        {
            _articleDal.Add(entity);
            // addfield
            return new SuccessResult(Messages.Article_Add);
        }

        public IResult Delete(Article entity)
        {
            _articleDal.Delete(entity);
            return new SuccessResult(Messages.Article_Deleted);
        }

        public IDataResult<List<Article>> GetAll()
        {
            return new SuccessDataResult<List<Article>>(_articleDal.GetAll(), Messages.Articleses_Listed);
        }

        public IDataResult<Article> GetById(int id)
        {
            return new SuccessDataResult<Article>(_articleDal.Get(x => x.ID == id), Messages.Articleem_Listed);
        }

        public IResult Update(Article entity)
        {
            _articleDal.Update(entity);
            return new SuccessResult(Messages.Article_Edit);
        }
    }
}
