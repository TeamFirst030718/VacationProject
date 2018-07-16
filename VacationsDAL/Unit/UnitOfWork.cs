using System;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsDAL.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VacationsContext _dbContext;

        private  EmployeesRepository _employees;

        private  JobTitleRepository _jotTitles;

        private  TransactionRepository _transactions;

        private  TeamRepository _teams;

        private  TransactionTypeRepository _transactionTypes;

        private  VacationRepository _vacations;

        private  VacationStatusTypeRepository _vacationStatusTypes;

        public UnitOfWork()
        {
            _dbContext = new VacationsContext();
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = new EmployeesRepository(_dbContext);
                }
                    
                return _employees;
            }
        }

        public IRepository<JobTitle> JobTitles
        {
            get
            {
                if (_jotTitles == null)
                {
                    _jotTitles = new JobTitleRepository(_dbContext);
                }

                return _jotTitles;
            }
        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                if (_transactions == null)
                {
                    _transactions = new TransactionRepository(_dbContext);
                }

                return _transactions;
            }
        }


        public IRepository<TransactionType> TransactionTypes
        {
            get
            {
                if (_transactionTypes == null)
                {
                    _transactionTypes = new TransactionTypeRepository(_dbContext);
                }

                return _transactionTypes;
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                if (_teams == null)
                {
                    _teams = new TeamRepository(_dbContext);
                }
                   
                return _teams;
            }
        }

        public IRepository<Vacation> Vacations
        {
            get
            {
                if (_vacations == null)
                {
                    _vacations = new VacationRepository(_dbContext);
                }

                return _vacations;
            }
        }

        public IRepository<VacationStatusType> VacationStatusTypes
        {
            get
            {
                if (_vacationStatusTypes == null)
                {
                    _vacationStatusTypes = new VacationStatusTypeRepository(_dbContext);
                }

                return _vacationStatusTypes;
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
