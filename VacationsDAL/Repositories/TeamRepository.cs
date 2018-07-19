using System.Collections.Generic;
using System.Data.Entity;
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

        public void Save()
        {
            if (_context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                                || e.State == EntityState.Modified
                                                || e.State == EntityState.Deleted))
            {
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
