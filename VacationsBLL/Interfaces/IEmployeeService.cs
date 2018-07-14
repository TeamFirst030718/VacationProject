using System;
using VacationsBLL.DTOs;

namespace VacationsBLL
{
    public interface IEmployeeService : IDisposable
    {
        void Create(EmployeeDTO employee);
    }
}