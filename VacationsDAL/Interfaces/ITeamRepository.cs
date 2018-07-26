using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        void Update(Team team);
        IEnumerable<Team> GetAll();
    }
}
