using System;
using VacationsDAL.Entities;
using VacationsDAL.Repositories;

namespace VacationsDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> Employees { get; }
        IRepository<JobTitle> JobTitles { get; }
        IRepository<Team> Teams { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<TransactionType> TransactionTypes { get; }
        IRepository<Vacation> Vacations { get; }
        IRepository<VacationStatusType> VacationStatusTypes { get; }
        IRepository<AspNetRole> AspNetRoles { get; }
        void Save();
    }
}
