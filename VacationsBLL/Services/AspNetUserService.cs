using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.Services
{
    public class AspNetUserService : IAspNetUserService
    {
        AspNetUsersRepository _users;

        public AspNetUserService(AspNetUsersRepository users)
        {
            _users = users;
        }

        public bool AspNetUserExists(string id)
        {
            return _users.GetById(id) != null;
        }
    }
}
