using System;
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

        public VacationStatusType[] Get(Func<VacationStatusType, bool> whereCondition = null)
        {
            IQueryable<VacationStatusType> baseCondition = _context.VacationStatusTypes;
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
        }

        public VacationStatusType GetById(string id)
        {
            return _context.VacationStatusTypes.FirstOrDefault(x => x.VacationStatusTypeID == id);
        }

        public VacationStatusType GetByType(string type)
        {
            return _context.VacationStatusTypes.FirstOrDefault(x => x.VacationStatusName.Equals(type));
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
    }
}
