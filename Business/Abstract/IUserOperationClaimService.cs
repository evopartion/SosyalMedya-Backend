using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Delete(int id, int claimId);
        IResult Add(UserOperationClaim entity);
        IResult Update(UserOperationClaim entity);
        IDataResult<List<UserOperationClaim>> GetAll();
    }
}
