using Core.Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;
using static Core.DataAccess.IEntityRepository;

namespace DataAccess.Abstract
{
    public interface IOperationClaimDal : IEntityRepository<OperationClaim>
    {
        List<ClaimDto> GetClaimById(Expression<Func<ClaimDto, bool>> filter=null);
    }
}
