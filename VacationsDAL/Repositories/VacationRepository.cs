using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        public Vacation[] Get(Func<Vacation, bool> whereCondition = null)
        {
            IQueryable<Vacation> baseCondition = _context.Vacations;
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
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
            try
            {
                _context.Vacations.Add(Vacation);
                _context.SaveChanges();
            }   
             catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    var a = string.Format("Object: " + validationError.Entry.Entity.ToString());


                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        var b = string.Format(err.ErrorMessage + "");

                    }
                }
            }
        }

        public void Update(Vacation vacation)
        {
            if (vacation != null)
            {
                
                    _context.Entry(vacation).State = EntityState.Modified;

                    _context.SaveChanges();
                
            }
        }
    }
}
