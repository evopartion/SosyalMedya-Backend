using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClamService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private int userId;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.UserClaimAdd);
        }

        public IResult Delete(int id, int claimId)
        {
            var deletedClaim = _userOperationClaimDal.Get(x => x.UserID == userId && x.OperationClaimID == claimId);
            if (deletedClaim != null)
            {
                _userOperationClaimDal.Delete(deletedClaim);
                return new SuccessResult(Messages.UserClaimDeleted);
            }
            return new ErrorResult(Messages.UserClaimNotFound);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(), Messages.UserClaimslisted);
        }

        public IResult Update(UserOperationClaim userOperationClaim)
        {
            
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserClaimUpdate);
        }
    }
}
