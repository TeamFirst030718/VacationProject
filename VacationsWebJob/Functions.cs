using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using VacationsBLL;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace VacationsWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        private static IVacationService _vacationService;
        private static IEmailSendService _emailSendService;
        private static IEmployeeService _employeeService;

        public Functions(IVacationService vacationService, IEmailSendService emailSendService, IEmployeeService employeeService)
        {
            _vacationService = vacationService;
            _emailSendService = emailSendService;
            _employeeService = employeeService;
        }

        [NoAutomaticTrigger]
        public static async Task ProcessMessage()
        {
            var vacations = _vacationService.GetVacations();

            var date = DateTime.Today;

            foreach (var vacationDto in vacations)
            {
                if ((date - vacationDto.DateOfBegin).Days == 14 &&  _vacationService.IsApproved(vacationDto.VacationStatusTypeID))
                {
                    var employee = _employeeService.GetUserById(vacationDto.EmployeeID);
                    var team = employee.EmployeesTeam.FirstOrDefault();
                    if (team != null)
                    {
                        var teamLeader = _employeeService.GetUserById(team.TeamLeadID);

                        await _emailSendService.SendAsync(teamLeader.WorkEmail, teamLeader.Name + " " + teamLeader.Surname, "Soon the vacation of your employee",
                            employee.Name + " " + employee.Surname, employee.Name + " " + employee.Surname + "will go on vacation after two weeks");
                    }
                }
            }
            

        }
    }
}
