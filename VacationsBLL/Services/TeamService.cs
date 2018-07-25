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

        private IMapService _mapService;

        public TeamService(IMapService mapService, ITeamRepository teams)
        {
            _mapService = mapService;

            _teams = teams;
        }

        public void CreateTeam(TeamDTO team)
        {
            _teams.Add(_mapService.Map<TeamDTO, Team>(team));
            
        }

    }
}
