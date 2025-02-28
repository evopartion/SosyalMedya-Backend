using Core.DataAccess;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserImageDal : EfEntityRepository<UserImage, SocialMediaContext>, IUserImageDal
    {
    }
}
