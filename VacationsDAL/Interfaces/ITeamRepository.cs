using System.Collections.Generic;
using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        void Update(Team team);
        IEnumerable<Team> GetAll();
        void RemoveEmployee(string EmployeeID, string TeamID);
        void AddEmployee(string EmployeeID, string TeamID);
        Team GetByName(string name);
    }
}
