﻿using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
        Task<IResult> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task<object?> AdminChangePassword(ChangePassword changePassword);
        //Task<IResult> ChangePassword(ChangePasswordModel updatedUser);
        //Task<IResult> AdminChangePassword(ChangePasswordModel changePassword);
    }
}
