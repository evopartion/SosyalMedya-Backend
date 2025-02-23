using Core.Entities;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DataAccess.IEntityRepository;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {

    }
}
