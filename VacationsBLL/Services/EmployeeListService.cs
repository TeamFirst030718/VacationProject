using System;
using System.Collections.Generic;
using System.Linq;
using VacationsBLL.DTOs;
using VacationsBLL.Enums;
using VacationsBLL.Functions;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class EmployeeListService : IEmployeeListService
    {
        private IEmployeeRepository _employees;
        private IVacationRepository _vacations;
        private IVacationStatusTypeRepository _vacationStatuses;

        public EmployeeListService(IEmployeeRepository employees,
                                   IVacationRepository vacations,
                                   IVacationStatusTypeRepository vacationStatuses)
        {
            _employees = employees;
            _vacations = vacations;
            _vacationStatuses = vacationStatuses;
        }

        public List<EmployeeListDTO> EmployeeList(string searchKey = null)
        {
            Employee[] employees = null;
            var today = DateTime.UtcNow;
            var statusApprovedID = _vacationStatuses.GetByType(VacationStatusTypeEnum.Approved.ToString()).VacationStatusTypeID;
            var vacations = _vacations.Get(x => x.VacationStatusTypeID.Equals(statusApprovedID));

            var result = new List<EmployeeListDTO>();

            if (searchKey != null)
            {
                bool whereLinq(Employee emp) => string.Format($"{emp.Name} {emp.Surname}").ToLower().Contains(searchKey.ToLower()) || emp.PhoneNumber.ToLower().Contains(searchKey.ToLower()) || emp.WorkEmail.ToLower().Contains(searchKey.ToLower());
                employees = _employees.Get(whereLinq);
            }
            else
            {
                employees = _employees.Get();
            }

            foreach (var employee in employees)
            {
                if (employee.EmployeesTeam.Count == 0)
                {
                    var temp = Mapper.Map<Employee, EmployeeForListDTO>(employee);
                    temp.CurrentVacationID = vacations.FirstOrDefault(x => x.EmployeeID.Equals(temp.EmployeeID) && x.DateOfBegin.Date <= today && x.DateOfEnd.Date >= today)?.VacationID;
                        result.Add(new EmployeeListDTO
                    {
                        EmployeeDto = temp,
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
                        var temp = Mapper.Map<Employee, EmployeeForListDTO>(employee);
                        temp.CurrentVacationID = vacations.FirstOrDefault(x => x.EmployeeID.Equals(temp.EmployeeID) && x.DateOfBegin.Date <= today && x.DateOfEnd.Date >= today)?.VacationID;

                        result.Add(new EmployeeListDTO
                        {
                            EmployeeDto = temp,
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
