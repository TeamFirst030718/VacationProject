using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IAspNetUserService
    {
        bool AspNetUserExists(string id);
        IEnumerable<AspNetUserDTO> GetUsers();
    }
}