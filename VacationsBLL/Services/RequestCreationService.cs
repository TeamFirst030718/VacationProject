using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;
using VacationsBLL.DTOs;
using VacationsDAL.Entities;
using System.Linq;
using System.Transactions;
using System;
using VacationsBLL.Enums;

namespace VacationsBLL.Services
{
    public class RequestCreationService : IRequestCreationService
    {
        private const string Empty = "None";
        private IVacationRepository _vacations;
        private IJobTitleRepository _jobTitles;
        private IEmployeeRepository _employees;
        private IVacationStatusTypeRepository _vacationStatusTypes;
        private IEmailSendService _emailService;

        public string GetStatusIdByType(string type)
        {
            return _vacationStatusTypes.GetByType(type).VacationStatusTypeID;
        }

        public RequestCreationService(IVacationRepository vacations,
                                      IVacationStatusTypeRepository vacationStatusTypes,
                                      IEmployeeRepository employees,
                                      IEmailSendService emailService,
                                      IJobTitleRepository jobTitles)
        {
            _vacations = vacations;
            _vacationStatusTypes = vacationStatusTypes;
            _employees = employees;
            _emailService = emailService;
            _jobTitles = jobTitles;
        }

        public void CreateVacation(VacationDTO vacation)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                _vacations.Add(Mapper.Map<VacationDTO, Vacation>(vacation));

                var employee = _employees.GetById(vacation.EmployeeID);

                if (employee.EmployeesTeam.Count > 0)
                {
                    var team = employee.EmployeesTeam.First();
                    var teamLead = _employees.GetById(team.TeamLeadID);

                    _emailService.SendAsync(teamLead.WorkEmail, $"{teamLead.Name} {teamLead.Surname}", "Vacation request.", "Employee has requested a vacation.",
                            $"{teamLead.Name} {teamLead.Surname}, your employee, {employee.Name} {teamLead.Surname}, has requested a vacation from {vacation.DateOfBegin.ToString("dd-MM-yyyy")} to {vacation.DateOfEnd.ToString("dd-MM-yyyy")}. Please, check it.");
                }

                scope.Complete();
            }
        }

        public void ForceVacationForEmployee(VacationDTO request)
        {
            request.VacationID = Guid.NewGuid().ToString();
            request.VacationStatusTypeID = GetStatusIdByType(VacationStatusTypeEnum.Pending.ToString());
            request.Created = DateTime.UtcNow;
            CreateVacation(request);
        }

        public RequestForEmployeeDTO GetEmployeeDataForRequestByID(string id)
        {
            var employee = _employees.GetById(id);
            if (employee != null)
            {
                var jobTitle = _jobTitles.GetById(employee.JobTitleID).JobTitleName;

                var request = new RequestForEmployeeDTO
                {
                    EmployeeID = employee.EmployeeID,                
                    EmployeeName = string.Format($"{employee.Name} {employee.Surname}"),
                    JobTitle = jobTitle,
                    TeamLeadName = employee.EmployeesTeam.Count.Equals(0) ? Empty : _employees.GetById(employee.EmployeesTeam.First().TeamLeadID).Name,
                    TeamName = employee.EmployeesTeam.Count.Equals(0) ? Empty : employee.EmployeesTeam.First().TeamName,
                };

                return request;
            }
            else
            {
                return new RequestForEmployeeDTO();
            }
        }
    }
}
