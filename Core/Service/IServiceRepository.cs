﻿using Core.Utilities.Result.Abstract;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IServiceRepository<T>
    {
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(int id);
        IResult Update(T entity);
        IResult Delete(T entity);
        IResult Add(T entity);
    }
}
