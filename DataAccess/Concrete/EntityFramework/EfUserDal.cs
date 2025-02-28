using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Entities;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepository<User, SocialMediaContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new SocialMediaContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.ID equals userOperationClaim.OperationClaimID
                             where userOperationClaim.UserID == user.ID
                             select new OperationClaim { ID = operationClaim.ID, Name = operationClaim.Name };
                return result.ToList();
            }
        }

        public List<UserDto> GetUsersDtos(Expression<Func<UserDto, bool>> filter = null)
        {
            using (var context = new SocialMediaContext())
            {
                var result = from user in context.Users
                             join userImage in context.UserImages on user.ID equals userImage.UserId into userImagesGroup
                             from userImage in userImagesGroup.DefaultIfEmpty()

                             select new UserDto
                             {
                                 Id = user.ID,
                                 Email = user.Email,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Gender = user.Gender,
                                 PhoneNumber = user.PhoneNumber,
                                 ImageId = userImage.Id,
                                 ImagePath = userImage != null ? userImage.ImagePath : string.Empty
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
