using System;
using System.Collections.Generic;

namespace VacationsDAL.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Remove(string id);
    }
}