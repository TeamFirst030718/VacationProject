using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using System.Linq;
using System.Web.UI.WebControls;
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

        private IMapService _mapService;

        public EmployeeService(IEmployeeRepository employees, IJobTitleRepository jobTitles, IMapService mapService, ITeamRepository teams)
        {
            _employees = employees;

            _jobTitles = jobTitles;

            _mapService = mapService;

            _teams = teams;
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
            var employeeToUpdate = _employees.GetAll().FirstOrDefault(x => x.EmployeeID == employee.EmployeeID);

            MapChanges(employeeToUpdate, employee);

            _employees.Update(employeeToUpdate);
        }

        private void MapChanges(Employee entity, EmployeeDTO changes)
        {
            var entityChanges = _mapService.Map<EmployeeDTO, Employee>(changes);
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

        public List<EmployeeListDTO> EmployeeList()
        {
            var list = _employees.GetAll();

            var result = new List<EmployeeListDTO>();

            foreach (var employee in list)
            {
                if (employee.EmployeesTeam.Count == 0 && employee.Teams.Count == 0)
                {
                    result.Add(new EmployeeListDTO
                    {
                        EmployeeDto = _mapService.Map<Employee,EmployeeDTO>(employee),
                        TeamDto = new TeamDTO
                        {
                            TeamID = "Empty",
                            TeamLeadID = "Empty",
                            TeamName = "Empty"
                        }
                    });
                }
                else if (employee.Teams.Count != 0)
                {
                    foreach (var team in employee.Teams)
                    {
                        result.Add(new EmployeeListDTO
                        {
                            EmployeeDto = _mapService.Map<Employee, EmployeeDTO>(employee),
                            TeamDto = new TeamDTO
                            {

                                TeamID = team.TeamID,
                                TeamLeadID = team.TeamLeadID,
                                TeamName = team.TeamName
                            }
                        });

                    }
                }
                else
                {
                    foreach (var team in employee.EmployeesTeam)
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

        public string GetTeamNameById(string id)
        {
            return _teams.GetById(id).TeamName;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
           
            return _mapService.MapCollection<Employee, EmployeeDTO>(_employees.GetAll());
        }

        public void AddToTeam(string EmployeeID, string TeamID)
        {
            var team = _teams.GetById(TeamID);
            var employee = _employees.GetById(EmployeeID);
           
            team.Employees.Add(employee);

            _teams.Update(team);
            
        }

        public IEnumerable<EmployeeDTO> GetAllFreeEmployees()
        {
            return _mapService.MapCollection<Employee, EmployeeDTO>(_employees.GetAll().Where(x=>x.EmployeesTeam.Count == 0));
        }
    }
}
