using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface ITeamService
    {
        void CreateTeam(TeamDTO team);
        IEnumerable<TeamListDTO> GetAllTeams();
        TeamListDTO GetById(string id);
        TeamDTO GetTeamById(string id);
        void UpdateTeamInfo(TeamDTO team);
        bool IsTeamLead(string id);
        void DeleteTeam(string teamId);
    }
}