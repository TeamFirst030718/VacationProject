using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class AspNetUsersRepository : IAspNetUsersRepository
    {
        private readonly VacationsContext _context;

        public AspNetUsersRepository(VacationsContext context)
        {
            _context = context;
        }

        public IEnumerable<AspNetUser> GetAll()
        {
            return _context.AspNetUsers.ToList();
        }

        public AspNetUser GetById(string id)
        {
            return _context.AspNetUsers.FirstOrDefault(x => x.Id == id);
        }

        public AspNetUser GetByUserName(string email)
        {
            return _context.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(email));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
