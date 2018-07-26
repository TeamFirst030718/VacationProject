using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class VacationTypeRepository : IVacationTypeRepository
    {
        private readonly VacationsContext _context;

        public VacationTypeRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public VacationType[] Get(Func<VacationType, bool> whereCondition = null)
        {
            IQueryable<VacationType> baseCondition = _context.VacationTypes;
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
        }

        public VacationType GetById(string id)
        {
            return _context.VacationTypes.FirstOrDefault(x => x.VacationTypeID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.VacationTypes.FirstOrDefault(x => x.VacationTypeID == id);

            if (obj != null)
            {
                _context.VacationTypes.Remove(obj);
                _context.SaveChanges();
            }
        }

        public void Add(VacationType VacationType)
        {
            _context.VacationTypes.Add(VacationType);
            _context.SaveChanges();
        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
