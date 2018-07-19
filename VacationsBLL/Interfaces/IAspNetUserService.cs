using System;

namespace VacationsBLL.Interfaces
{
    public interface IAspNetUserService : IDisposable
    {
        bool AspNetUserExists(string id);
    }
}