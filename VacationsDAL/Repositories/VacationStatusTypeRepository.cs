using System.Collections.Generic;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class VacationStatusTypeRepository : IRepository<VacationStatusType>
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
            }
        }

        public void Add(VacationStatusType VacationStatusType)
        {
            _context.VacationStatusTypes.Add(VacationStatusType);
        }
    }
}
