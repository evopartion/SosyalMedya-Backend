using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVerificationCodeDal : EfEntityRepositoryBase<VerificationCode, SocialMediaContext>, IVerificationCodeDal
    {
    }
}
