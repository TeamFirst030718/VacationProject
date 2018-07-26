using System.Collections.Generic;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class AdminEmployeeListService : IAdminEmployeeListService
    {
        private IEmployeeRepository _employees;

        public AdminEmployeeListService(IEmployeeRepository employees, IJobTitleRepository jobTitles)
        {
            _employees = employees;
        }

        public List<EmployeeListDTO> EmployeeList()
        {
            var list = _employees.Get();

            var result = new List<EmployeeListDTO>();

            foreach (var employee in list)
            {
                if (employee.Teams.Count == 0)
                {
                    result.Add(new EmployeeListDTO
                    {
                        EmployeeDto = Mapper.Map<Employee, EmployeeDTO>(employee),
                        TeamDto = new TeamDTO
                        {
                            TeamID = "Empty",
                            TeamLeadID = "Empty",
                            TeamName = "Empty"
                        }
                    });
                }
                else
                {
                    foreach (var team in employee.Teams)
                    {
                        result.Add(new EmployeeListDTO
                        {
                            EmployeeDto = Mapper.Map<Employee, EmployeeDTO>(employee),
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
    }
}
