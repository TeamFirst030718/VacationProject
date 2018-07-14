using System;
using VacationsBLL.DTOs;
using VacationsDAL.Entities;

namespace VacationsBLL
{
    public interface IEmployeeService : IDisposable
    {
        void Create(EmployeeDTO employee);
        EmployeeDTO GetUserById(string id);
    }
}