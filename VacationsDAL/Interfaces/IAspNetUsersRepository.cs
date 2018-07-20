using System;
using System.Collections.Generic;
using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IAspNetUsersRepository :IDisposable
    {
        IEnumerable<AspNetUser> GetAll();
        AspNetUser GetById(string id);
        AspNetUser GetByUserName(string email);
    }
}