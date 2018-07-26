using System;
using System.Collections.Generic;
using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IUsersRepository :IDisposable

    {
        AspNetUser[] Get(Func<AspNetUser, bool> condition = null);
        AspNetUser GetById(string id);
        AspNetUser GetByUserName(string email);
    }
}