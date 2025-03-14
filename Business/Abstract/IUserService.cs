﻿using Core.Entities.Concrete;
using Core.Service;
using Core.Utilities.Result.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService : IServiceRepository<User>
    {
        IDataResult<List<User>> GetAll();
        IDataResult<List<UserDto>> GetAllDto();
        IDataResult<User> GetUserById(int userId);
        IDataResult<UserDto> GetUserDtoById(int userId);
        IResult Add(User user);
        IResult Delete(int userId);
        IResult Update(User user);
        IResult UpdateByDto(UserDto userDto);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        Task<IDataResult<User>> GetUserByMail(string email);
        IDataResult<UserDto> GetUserDtoByMail(string email);
    }

}
