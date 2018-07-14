using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Entities;
using VacationsDAL.Repositories;

namespace VacationsDAL.Interfaces
{
    public interface IEmployeeUnitOfWork : IUnitOfWork
    {
        IRepository<Employee> Employees { get;}
    }
}
