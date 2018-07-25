using System;
using System.Collections.Generic;

namespace VacationsDAL.Interfaces
{
    public interface IRepository<T>: IDisposable
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Remove(string id);
    }
}