using VacationsDAL.Repositories;

namespace VacationsBLL
{
    public class AspNetUserService
    {
        private readonly AspNetUsersRepository _employeesRepository;

        public AspNetUserService()
        {
            _employeesRepository = new AspNetUsersRepository();
        }

        public bool AspNetUserExists(string id)
        {
            return _employeesRepository.GetById(id) != null;
        }
    }
}
