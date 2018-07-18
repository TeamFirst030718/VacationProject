using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class AspNetRolesRepository: IRepository<AspNetRole>
    {
        private readonly VacationsContext _context;

        public AspNetRolesRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<AspNetRole> GetAll()
        {
            return _context.AspNetRoles.ToList();
        }

        public AspNetRole GetById(string id)
        {
            return _context.AspNetRoles.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(string id)
        {
            var obj = _context.AspNetRoles.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                _context.AspNetRoles.Remove(obj);
            }
        }

        public void Add(AspNetRole aspNetRole)
        {
            _context.AspNetRoles.Add(aspNetRole);
        }
    }
}
