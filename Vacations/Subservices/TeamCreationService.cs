using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace Vacations.Subservices
{
    public static class TeamCreationService
    {
        public static void RegisterTeam(TeamViewModel model,string employees, IEmployeeService employeeService, ITeamService teamService)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                teamService.CreateTeam(Mapper.Map<TeamViewModel, TeamDTO>(new TeamViewModel
                {
                    TeamLeadID = model.TeamLeadID,
                    TeamID = model.TeamID,
                    TeamName = model.TeamName
                }));
                string members = employees;
                if (members != null)
                {
                    var result = members.Split(',');
                    foreach (var employeeId in result)
                    {
                        if (employeeId != model.TeamLeadID)
                        {
                            employeeService.AddToTeam(employeeId, model.TeamID);
                        }
                    }
                }

                transaction.Complete();
            }
        }
    }
}