using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        void Create(EmployeeDTO employee);
        EmployeeDTO GetUserById(string id);
        List<JobTitleDTO> GetJobTitles();
        string GetJobTitleIdByName(string jobTitleName);
        void UpdateEmployee(EmployeeDTO employee);
    }
}