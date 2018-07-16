using System.Collections.Generic;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class VacationRepository : IRepository<Vacation>
    {
        private readonly VacationsContext _context;

        public VacationRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Vacation> GetAll()
        {
            return _context.Vacations.ToList();
        }

        public Vacation GetById(string id)
        {
            return _context.Vacations.FirstOrDefault(x => x.VacationID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.Vacations.FirstOrDefault(x => x.VacationID == id);

            if (obj != null)
            {
                _context.Vacations.Remove(obj);
            }
        }

        public void Add(Vacation Vacation)
        {
            _context.Vacations.Add(Vacation);
        }
    }
}
