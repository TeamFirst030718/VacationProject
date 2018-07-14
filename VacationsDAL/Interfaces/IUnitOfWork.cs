using System;

namespace VacationsDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
