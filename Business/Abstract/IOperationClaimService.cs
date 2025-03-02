using Core.Entities.Concrete;
using Core.Service;
using Core.Utilities.Result.Abstract;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IOperationClaimService : IServiceRepository<OperationClaim>
    {
        IDataResult<List<ClaimDto>> GetClaimByUsers(int claimId);
    }
}
