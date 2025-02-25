using Core.Entities.Concrete;
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
    public interface IUserServices : IServiceRepository<User>
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetUserByMail(string email);
        IResult UpdateByDto(UserDto userDto);
        IDataResult<List<UserDto>> GetAllDto();
        IDataResult<UserDto> GetUserDtoById(int userId);
    }

}
