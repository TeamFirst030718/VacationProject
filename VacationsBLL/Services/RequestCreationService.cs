using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;
using VacationsBLL.DTOs;
using VacationsDAL.Entities;
using System.Linq;

namespace VacationsBLL.Services
{
    public class RequestCreationService : IRequestCreationService
    {
        private IVacationRepository _vacations;
        private IEmployeeRepository _employees;
        private IVacationStatusTypeRepository _vacationStatusTypes;
        private IEmailSendService _emailService;

        public string GetStatusIdByType(string type)
        {
            return _vacationStatusTypes.GetByType(type).VacationStatusTypeID;
        }

        public RequestCreationService(IVacationRepository vacations, IVacationStatusTypeRepository vacationStatusTypes, IEmployeeRepository employees, IEmailSendService emailService)
        {
            _vacations = vacations;
            _vacationStatusTypes = vacationStatusTypes;
            _employees = employees;
            _emailService = emailService;
        }

        public void CreateVacation(VacationDTO vacation)
        {
            _vacations.Add(Mapper.Map<VacationDTO, Vacation>(vacation));

            var employee = _employees.GetById(vacation.EmployeeID);

            if (!employee.EmployeesTeam.Count.Equals(0))
            {
                var team = employee.EmployeesTeam.First();
                var teamLead = _employees.GetById(team.TeamLeadID);

                _emailService.SendAsync(teamLead.WorkEmail, $"{teamLead.Name} {teamLead.Surname}", "Vacation request.", "Employee has requested a vacation.",
                        $"{teamLead.Name} {teamLead.Surname}, your employee, {employee.Name} {teamLead.Surname}, has requested a vacation from {vacation.DateOfBegin.ToString("dd-MM-yyyy")} to {vacation.DateOfEnd.ToString("dd-MM-yyyy")}. Please, check it.");
            }

        }
    }
}
