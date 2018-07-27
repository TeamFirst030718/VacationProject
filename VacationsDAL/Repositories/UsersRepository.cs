using System;
using System.Linq;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly VacationsContext _context;

        public UsersRepository(VacationsContext context)
        {
            _context = context;
        }

        public AspNetUser[] Get(Func<AspNetUser, bool> whereCondition = null)
        {
            IQueryable<AspNetUser> baseCondition = _context.AspNetUsers;
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
        }

        public AspNetUser GetById(string id)
        {
            return _context.AspNetUsers.FirstOrDefault(x => x.Id == id);
        }

        public AspNetUser GetByUserName(string email)
        {
            return _context.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(email));
        }
    }
}
