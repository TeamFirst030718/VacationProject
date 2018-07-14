using System;
using VacationsBLL.Models;

namespace VacationsBLL
{
    public interface IEmployeeService : IDisposable
    {
        void Create(EmployeeDTO employee);
    }
}