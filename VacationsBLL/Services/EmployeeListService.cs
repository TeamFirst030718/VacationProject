using System.Collections.Generic;
using System.Linq;
using VacationsBLL.DTOs;
using VacationsBLL.Functions;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class EmployeeListService : IEmployeeListService
    {
        private IEmployeeRepository _employees;

        public EmployeeListService(IEmployeeRepository employees, IJobTitleRepository jobTitles)
        {
            _employees = employees;
        }

        public List<EmployeeListDTO> EmployeeList()
        {
            var list = _employees.Get();

            var result = new List<EmployeeListDTO>();

            foreach (var employee in list)
            {
                if (employee.EmployeesTeam.Count == 0)
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
                    foreach (var team in employee.EmployeesTeam)
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

            return result.OrderBy(x=> FunctionHelper.EmployeeSortFunc(x.TeamDto.TeamName)).ToList();
        }
    }
}
