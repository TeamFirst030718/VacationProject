using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
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

        private IMapService _mapService;

        public EmployeeService(IEmployeeRepository employees, IJobTitleRepository jobTitles, IMapService mapService)
        {
            _employees = employees;

            _jobTitles = jobTitles;

            _mapService = mapService;
        }

        public void Create(EmployeeDTO employee)
        {
            _employees.Add(_mapService.Map<EmployeeDTO, Employee>(employee));
        }

        public EmployeeDTO GetUserById(string id)
        {
            return _mapService.Map<Employee,EmployeeDTO>(_employees.GetById(id)); 
        }

        public List<JobTitleDTO> GetJobTitles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobTitle, JobTitleDTO>()).CreateMapper();

            var jobTitles = _jobTitles.GetAll();

            return jobTitles.Select(jobTitle => mapper.Map<JobTitle, JobTitleDTO>(jobTitle)).ToList();
        }

        public string GetJobTitleIdByName(string jobTitleName)
        {
            return _jobTitles.GetAll().FirstOrDefault(x => x.JobTitleName.Equals(jobTitleName)).JobTitleID;
        }

        public void UpdateEmployee(EmployeeDTO employee)
        {
            var employeeToUpdate = _employees.GetAll().FirstOrDefault(x => x.WorkEmail == employee.WorkEmail);

            if (employeeToUpdate!=null)
            {
                employeeToUpdate = _mapService.Map<EmployeeDTO, Employee>(employee);
            }

            _employees.Update(employeeToUpdate);
        }

        public List<EmployeeListDTO> EmployeeList()
        {
            var list = _employees.GetAll();

            var result = new List<EmployeeListDTO>();

            foreach (var employee in list)
            {
                if (employee.Teams.Count == 0)
                {
                    result.Add(new EmployeeListDTO
                    {
                        EmployeeDto = _mapService.Map<Employee,EmployeeDTO>(employee),
                        TeamDto = null
                    });
                }
                else
                {
                    foreach (var team in employee.Teams)
                    {
                        result.Add(new EmployeeListDTO
                        {
                            EmployeeDto = _mapService.Map<Employee,EmployeeDTO>(employee),
                            TeamDto = new TeamDTO
                            {
                                TeamID = team.TeamID,
                                TeamLeadID = team.TeamLeadID,
                                TeamName = team.TeamName
                            }
                        });

                    }
                } 
            }

            return result;
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _employees.Dispose();
        }
    }
}
