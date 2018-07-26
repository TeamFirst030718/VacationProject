﻿using System;
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

        public IEnumerable<TeamListDTO> GetAllTeams()
        {
            var result = new List<TeamListDTO>();
            var teams = _teams.GetAll();

            foreach (var team in teams)
            {
                result.Add(new TeamListDTO
                {
                    TeamID = team.TeamID,
                    TeamLeadID = team.TeamLeadID,
                    TeamName = team.TeamName,
                    AmountOfEmployees = team.Employees.Count
                } );
            }
            return result;
        }

        public TeamListDTO GetById(string id)
        {
            var team = _teams.GetById(id);
            
            var result = new TeamListDTO
            {
                TeamID = team.TeamID,
                TeamLeadID = team.TeamLeadID,
                TeamName = team.TeamName,
                AmountOfEmployees = team.Employees.Count
            };

            return result;
        }

    }
}