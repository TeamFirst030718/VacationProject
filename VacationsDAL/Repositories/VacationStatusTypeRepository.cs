using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class VacationStatusTypeRepository : IVacationStatusTypeRepository
    {
        private readonly VacationsContext _context;

        public VacationStatusTypeRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<VacationStatusType> GetAll()
        {
            return _context.VacationStatusTypes.ToList();
        }

        public VacationStatusType GetById(string id)
        {
            return _context.VacationStatusTypes.FirstOrDefault(x => x.VacationStatusTypeID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.VacationStatusTypes.FirstOrDefault(x => x.VacationStatusTypeID == id);

            if (obj != null)
            {
                _context.VacationStatusTypes.Remove(obj);
                _context.SaveChanges();
            }
        }

        public void Add(VacationStatusType VacationStatusType)
        {
            _context.VacationStatusTypes.Add(VacationStatusType);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
