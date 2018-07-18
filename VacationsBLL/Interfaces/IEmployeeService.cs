using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL
{
    public interface IEmployeeService : IDisposable
    {
        void SaveChanges();
        void Create(EmployeeDTO employee);
        EmployeeDTO GetUserById(string id);
        List<JobTitleDTO> GetJobTitles();
        string GetJobTitleIdByName(string jobTitleName);
    }
}