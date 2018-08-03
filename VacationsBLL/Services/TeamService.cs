using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<TeamListDTO> GetAllTeams(string searchKey = null)
        {
            Team[] teams = null;
            var result = new List<TeamListDTO>();

            if (searchKey != null)
            {
                bool whereLinq(Team team) => team.TeamName.ToLower().Contains(searchKey.ToLower());
                teams = _teams.Get(whereLinq);
            }
            else
            {
                teams = _teams.Get();
            }

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
            
            if(team != null)
            {
                var result = new TeamListDTO
                {
                    TeamID = team.TeamID,
                    TeamLeadID = team.TeamLeadID,
                    TeamName = team.TeamName,
                    AmountOfEmployees = team.Employees.Count
                };

                return result;
            }

            return new TeamListDTO();
        }

        public void DeleteTeam(string teamId)
        {
            _teams.Remove(teamId);
        }

        public TeamDTO GetTeamById(string id)
        {
            return Mapper.Map<Team, TeamDTO>(_teams.GetById(id));
        }

        public void UpdateTeamInfo(TeamDTO team)
        {
            _teams.Update(Mapper.Map<TeamDTO, Team>(team));
        }
    }
}
