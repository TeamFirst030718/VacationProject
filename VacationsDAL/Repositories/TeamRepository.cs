using System;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly VacationsContext _context;

        public TeamRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public Team[] Get(Func<Team, bool> whereCondition = null)
        {
            IQueryable<Team> baseCondition = _context.Teams;
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
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
                _context.SaveChanges();
            }
        }

        public void Add(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void Update(Team team)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
