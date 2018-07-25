using System.Collections.Generic;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.Services
{
    public class AspNetUserService : IAspNetUserService
    {
        private IAspNetUsersRepository _users;
        private IMapService _mapService;

        public AspNetUserService(IAspNetUsersRepository users, IMapService mapService)
        {
            _users = users;
            _mapService = mapService;
        }

        public bool AspNetUserExists(string id)
        {
            return _users.GetById(id) != null;
        }

        public IEnumerable<AspNetUserDTO> GetUsers()
        {
            return _mapService.MapCollection<AspNetUser, AspNetUserDTO>(_users.GetAll());
        }

        public void Dispose()
        {
            _users.Dispose();
        }
    }
}
