using System;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsDAL.Unit
{
    public class EmployeeUnitOfWork : IEmployeeUnitOfWork
    {
        protected VacationsContext _dbContext;

        protected EmployeesRepository _employees;

        public EmployeeUnitOfWork()
        {
            _dbContext = new VacationsContext();
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (_employees == null)
                    _employees = new EmployeesRepository(_dbContext);
                return _employees;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
