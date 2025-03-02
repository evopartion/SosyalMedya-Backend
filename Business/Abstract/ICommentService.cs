using Core.Service;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{   public interface ICommentService : IServiceRepository<Comment>
    {
        IDataResult<List<Comment>> TrueComment();
        IDataResult<List<Comment>> NotSeen(int id);
        IDataResult<List<Comment>> FalseComment();
    }
}
