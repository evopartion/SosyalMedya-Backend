using Entities.Concrete;
using static Core.DataAccess.IEntityRepository;

namespace DataAccess.Abstract
{
    public interface IArticleDal : IEntityRepository<Article>
    {

    }
}
