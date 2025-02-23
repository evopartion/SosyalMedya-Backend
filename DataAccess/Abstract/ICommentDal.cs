using Entities.Concrete;
using static Core.DataAccess.IEntityRepository;

namespace DataAccess.Abstract
{
    public interface ICommentDal : IEntityRepository<Comment>
    {

    }
}
