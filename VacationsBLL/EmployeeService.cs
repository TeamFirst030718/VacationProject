using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Models;
using VacationsContext.Entities;
using VacationsDAL.Repositories;

namespace VacationsBLL
{
    public class EmployeeService
    {
        private readonly EmployeesRepository _employeesRepository;

        public EmployeeService()
        {
            _employeesRepository = new EmployeesRepository();
        }

        public void Create(EmployeeDTO employee)
        {
            _employeesRepository.Add(new Employee
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                PersonalMail = employee.PersonalMail,
                Skype = employee.Skype,
                HireDate = employee.HireDate,
                Status = employee.Status,
                DateOfDismissal = employee.DateOfDismissal,
                VacationBalance = employee.VacationBalance,
                JobTitleID = employee.JobTitleID
            });
        }
    }
}
