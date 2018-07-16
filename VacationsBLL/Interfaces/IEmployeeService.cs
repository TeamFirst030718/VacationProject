using System;
using VacationsBLL.DTOs;

namespace VacationsBLL
{
    public interface IEmployeeService : IDisposable
    {
        void SaveChanges();
        void Create(EmployeeDTO employee);
        EmployeeDTO GetUserById(string id);
        string GetJobTitleIdByName(string jobTitleName);
    }
}