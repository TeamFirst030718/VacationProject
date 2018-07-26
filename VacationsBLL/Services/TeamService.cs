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
    public class TeamService : ITeamService
    {
        private ITeamRepository _teams;

        public TeamService(ITeamRepository teams)
        {
            _teams = teams;
        }

        public void CreateTeam(TeamDTO team)
        {
            _teams.Add(Mapper.Map<TeamDTO, Team>(team));
            
        }

    }
}
