using VacationsDAL.Entities;
using VacationsDAL.Repositories;

namespace VacationsDAL.Interfaces
{
    public interface IEmployeeUnitOfWork : IUnitOfWork
    {
        IRepository<Employee> Employees { get;}
    }
}
