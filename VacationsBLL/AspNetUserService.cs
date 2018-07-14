using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Models;
using VacationsDAL.Entities;
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
