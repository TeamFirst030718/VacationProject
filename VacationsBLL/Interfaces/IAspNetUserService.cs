using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IAspNetUserService: IDisposable
    {
        bool AspNetUserExists(string id);
        IEnumerable<AspNetUserDTO> GetUsers();
    }
}