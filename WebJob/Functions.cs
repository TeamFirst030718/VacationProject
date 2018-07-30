using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Interfaces;

namespace WebJob
{
    public class Functions
    {
        private static IVacationService _vacationService;
        private static IEmailSendService _emailSendService;
        private static IEmployeeService _employeeService;

        public Functions(IVacationService vacationService, IEmailSendService emailSendService,
            IEmployeeService employeeService)
        {
            _vacationService = vacationService;
            _emailSendService = emailSendService;
            _employeeService = employeeService;
        }


        public void ProcessMessage()
        {
            var vacations = _vacationService.GetVacations();

            var date = DateTime.Today;

            foreach (var vacationDto in vacations)
            {
                if ((date - vacationDto.DateOfBegin).Days == 14 &&
                    _vacationService.IsApproved(vacationDto.VacationStatusTypeID))
                {
                    var employee = _employeeService.GetUserById(vacationDto.EmployeeID);
                    var team = employee.EmployeesTeam.FirstOrDefault();
                    if (team != null)
                    {
                        var teamLeader = _employeeService.GetUserById(team.TeamLeadID);

                         _emailSendService.SendAsync(teamLeader.WorkEmail,
                            teamLeader.Name + " " + teamLeader.Surname, "Soon the vacation of your employee",
                            employee.Name + " " + employee.Surname,
                            employee.Name + " " + employee.Surname + "will go on vacation after two weeks");
                    }
                }
            }

        }
    }
}
