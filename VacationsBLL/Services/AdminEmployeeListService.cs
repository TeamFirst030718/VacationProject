using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class AdminEmployeeListService : IAdminEmployeeListService
    {
        private IEmployeeRepository _employees;

        private IMapService _mapService;

        public AdminEmployeeListService(IEmployeeRepository employees, IJobTitleRepository jobTitles, IMapService mapService)
        {
            _employees = employees;

            _mapService = mapService;
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
                        EmployeeDto = _mapService.Map<Employee, EmployeeDTO>(employee),
                        TeamDto = null
                    });
                }
                else
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
            }

            return result;
        }
    }
}
