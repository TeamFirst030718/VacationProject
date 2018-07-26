using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        bool AspNetUserExists(string id);
        UserDTO[] GetUsers();
    }
}