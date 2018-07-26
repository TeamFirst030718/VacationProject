using System.Collections.Generic;
using System.Linq;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employees;

        private IJobTitleRepository _jobTitles;

        private ITeamRepository _teams;

        public EmployeeService(IEmployeeRepository employees,
                               IJobTitleRepository jobTitles,
                               ITeamRepository teams)
        {
            _employees = employees;

            _jobTitles = jobTitles;

            _teams = teams;
        }

        public void Create(EmployeeDTO employee)
        {
            _employees.Add(Mapper.Map<EmployeeDTO, Employee>(employee));
        }

        public EmployeeDTO GetUserById(string id)
        {
            return Mapper.Map<Employee, EmployeeDTO>(_employees.GetById(id)); 
        }

        public List<JobTitleDTO> GetJobTitles()
        {
            var jobTitles = _jobTitles.Get();

            return jobTitles.Select(jobTitle => Mapper.Map<JobTitle, JobTitleDTO>(jobTitle)).ToList();
        }

        public string GetJobTitleIdByName(string jobTitleName)
        {
            return _jobTitles.Get().FirstOrDefault(x => x.JobTitleName.Equals(jobTitleName)).JobTitleID;
        }

        public EmployeeDTO[] GetEmployees()
        {
            return Mapper.MapCollection<Employee, EmployeeDTO>(_employees.Get());
        }

        public void UpdateEmployee(EmployeeDTO employee)
        {
            var employeeToUpdate = _employees.GetById(employee.EmployeeID);

            MapChanges(employeeToUpdate, employee);

            _employees.Update(employeeToUpdate);
        }

        private void MapChanges(Employee entity, EmployeeDTO changes)
        {
            var entityChanges = Mapper.Map<EmployeeDTO, Employee>(changes);
            entity.BirthDate = entityChanges.BirthDate;
            entity.DateOfDismissal = entityChanges.DateOfDismissal;
            entity.EmployeeID = entityChanges.EmployeeID;
            entity.HireDate = entityChanges.HireDate;
            entity.JobTitleID = entityChanges.JobTitleID;
            entity.Name = entityChanges.Name;
            entity.PersonalMail = entityChanges.PersonalMail;
            entity.PhoneNumber = entityChanges.PhoneNumber;
            entity.Skype = entityChanges.Skype;
            entity.Status = entityChanges.Status;
            entity.Surname = entityChanges.Surname;
            entity.VacationBalance = entityChanges.VacationBalance;
            entity.WorkEmail = entityChanges.WorkEmail;
        }

        public string GetTeamNameById(string id)
        {
            return _teams.GetById(id).TeamName;
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _employees.Dispose();
        }
    }
}
