﻿using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class UserService : IUserService
    {
        private IUsersRepository _users;

        public UserService(IUsersRepository users)
        {
            _users = users;
        }

        public bool AspNetUserExists(string id)
        {
            return _users.GetById(id) != null;
        }
		
        public UserDTO[] GetUsers()
        {
           return Mapper.MapCollection<AspNetUser, UserDTO>(_users.Get());
        }
    }
}
