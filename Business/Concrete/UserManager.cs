using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserServices
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User entity)
        {
            var rulesResult = BusinessRules.Run(CheckIfEmailExist(entity.Email));
            if (rulesResult != null)
            {
                return rulesResult;
            }
            _userDal.Add(entity);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User entity)
        {
            var rulesResult = BusinessRules.Run(CheckIfUserIdExist(entity.ID));
            if (rulesResult != null)
            {
                return rulesResult;
            }
            var deletedUser = _userDal.Get(x => x.ID == entity.ID);
            _userDal.Delete(deletedUser);
            return new SuccessResult(Messages.userDeleted);
        }

        public IResult DeleteByID(int userId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<List<UserDto>> GetAllDto()
        {
            return new SuccessDataResult<List<UserDto>>(_userDal.GetUsersDtos(), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(x => x.ID == id);
            if (user != null)
            {
                return new SuccessDataResult<User>(user, Messages.UserListed);
            }
            return new ErrorDataResult<User>(Messages.UserNotExist);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var rulesResult = BusinessRules.Run(CheckIfUserIdExist(user.ID));
            if (rulesResult != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(rulesResult.Message);
            }
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<User> GetUserByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.Email == email));
        }

        public IDataResult<UserDto> GetUserDtoById(int userId)
        {
            return new SuccessDataResult<UserDto>(_userDal.GetUsersDtos(x => x.ID == userId).SingleOrDefault(), Messages.UserListed);
        }

        public IResult Update(User entity)
        {
            var result = BusinessRules.Run(CheckIfUserIdExist(entity.ID), CheckIfEmailAvailable(entity.Email));
            if (result != null)
            {
                return result;
            }

            _userDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult UpdateByDto(UserDto userDto)
        {
            var rulesResult = BusinessRules.Run(CheckIfUserIdExist(userDto.ID), CheckIfEmailAvailable(userDto.Email));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var updatedUser = _userDal.Get(x => x.ID == userDto.ID && x.Email == userDto.Email);
            if (updatedUser == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            updatedUser.FirstName = userDto.FirstName;
            updatedUser.LastName = userDto.LastName;
            updatedUser.Email = userDto.Email;

            _userDal.Update(updatedUser);
            return new SuccessResult(Messages.UserUpdated);
        }

        //Business Rules

        private IResult CheckIfUserIdExist(int userId)
        {
            var result = _userDal.GetAll(x => x.ID == userId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.UserNotExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfEmailExist(string userEmail)
        {
            var result = BaseCheckIfEmailExist(userEmail);
            if (result)
            {
                return new ErrorResult(Messages.UserEmailExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfEmailAvailable(string userEmail)
        {
            var result = BaseCheckIfEmailExist(userEmail);
            if (!result)
            {
                return new ErrorResult(Messages.userEmailNotAvailable);
            }
            return new SuccessResult();
        }

        private bool BaseCheckIfEmailExist(string userEmail)
        {
            return _userDal.GetAll(x => x.Email == userEmail).Any();
        }
    }
}
