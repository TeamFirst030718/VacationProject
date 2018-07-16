using System.Collections.Generic;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly VacationsContext _context;

        public TeamRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        public Team GetById(string id)
        {
            return _context.Teams.FirstOrDefault(x => x.TeamID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.Teams.FirstOrDefault(x => x.TeamID == id);

            if (obj != null)
            {
                _context.Teams.Remove(obj);
            }
        }

        public void Add(Team Team)
        {
            _context.Teams.Add(Team);
        }
    }
}
