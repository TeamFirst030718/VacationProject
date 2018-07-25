using System;

namespace VacationsBLL.Interfaces
{
    public interface IAspNetUserService
    {
        bool AspNetUserExists(string id);
    }
}