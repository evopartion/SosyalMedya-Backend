using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepository<OperationClaim, SocialMediaContext>, IOperationClaimDal
    {
        public List<ClaimDto> GetClaimById(Expression<Func<ClaimDto, bool>> filter = null)
        {
            using (var context = new SocialMediaContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims on operationClaim.ID equals userOperationClaim.OperationClaimID
                             join user in context.Users on userOperationClaim.UserID equals user.ID
                             select new ClaimDto
                             {
                                 Id = userOperationClaim.ID,
                                 UserId = user.ID,
                                 UserName = user.FirstName + " " + user.LastName,
                                 OperationClaimId = operationClaim.ID,
                                 ClaimName = operationClaim.Name
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

    }
}
