using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class VacationRepository : IVacationRepository
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
                _context.SaveChanges();
            }
        }

        public void Add(Vacation Vacation)
        {
            _context.Vacations.Add(Vacation);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
