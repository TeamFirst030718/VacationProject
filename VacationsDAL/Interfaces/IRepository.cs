using System.Collections.Generic;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public interface IRepository<T>
    {
        void Add(T employee);
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Remove(string id);
    }
}