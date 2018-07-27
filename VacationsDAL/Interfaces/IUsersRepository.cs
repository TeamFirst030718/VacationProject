using System;
using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IUsersRepository

    {
        AspNetUser[] Get(Func<AspNetUser, bool> condition = null);
        AspNetUser GetById(string id);
        AspNetUser GetByUserName(string email);
    }
}