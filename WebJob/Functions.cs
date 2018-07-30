using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using VacationsBLL.Enums;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace WebJob
{
    public class Functions
    {
        private readonly string SendGridApiKeyName = "SendGridApiKey";

        private bool IsApproved(VacationsContext context, string id)
        {
            
                var obj = context.VacationStatusTypes.FirstOrDefault(x => x.VacationStatusTypeID == id);
                return obj != null && obj.VacationStatusName == VacationStatusTypeEnum.Approved.ToString();
            
        }

        private async Task SendAsync(string address, string name, string title, string plainTextContent, string message)
        {
            var apiKey = ConfigurationManager.AppSettings[SendGridApiKeyName];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Softheme Vacations");
            var subject = title;
            var to = new EmailAddress(address, name);
            var htmlContent = "<strong>" + message + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        private Employee GetUserById(VacationsContext context, string id)
        {
                var obj = context.Employees.FirstOrDefault(x => x.EmployeeID == id);
                return obj;
        }

        public async Task ProcessMessage()
        {
            using (var context = new VacationsContext())
            {
                var vacations = context.Vacations.ToList();

                var date = DateTime.Today;

                foreach (var vacation in vacations)
                {
                    if ((vacation.DateOfBegin - date).Days == 14 &&
                        IsApproved(context, vacation.VacationStatusTypeID))
                    {
                        var employee = GetUserById(context, vacation.EmployeeID);
                        var team = employee.EmployeesTeam.FirstOrDefault();
                        if (team != null)
                        {
                            var teamLeader = GetUserById(context, team.TeamLeadID);

                            await SendAsync(teamLeader.WorkEmail,
                                teamLeader.Name + " " + teamLeader.Surname, "Soon the vacation of your employee",
                                employee.Name + " " + employee.Surname,
                                employee.Name + " " + employee.Surname + "will go on vacation after two weeks");
                        }
                    }
                }
            }
        }
    }
}
