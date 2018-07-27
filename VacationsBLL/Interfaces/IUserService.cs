using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IUserService
    {
        bool AspNetUserExists(string id);
        UserDTO[] GetUsers();
    }
}