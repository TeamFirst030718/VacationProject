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

        public IEnumerable<VacationType> GetAll()
        {

            try
            {
                return _context.VacationTypes.ToList();
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

            return _context.VacationTypes.ToList();
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
