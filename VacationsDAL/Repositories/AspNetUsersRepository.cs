using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Entities;

namespace VacationsDAL.Repositories
{
    public class AspNetUsersRepository
    {
        private readonly VacationsContext _context;
        public AspNetUsersRepository()
        {
            _context = new VacationsContext();
        }

        public IEnumerable<AspNetUser> GetAll()
        {
            return _context.AspNetUsers.ToList();
        }

        public AspNetUser GetById(string id)
        {
            return _context.AspNetUsers.FirstOrDefault(x => x.Id == id);
        }

    }
}
