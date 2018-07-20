using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.Services
{
    public  class ProfileDataService : IProfileDataService
    {
        private IEmployeeRepository _employees;

        private IJobTitleRepository _jobTitles;

        private IAspNetUsersRepository _users;

        private IMapService _mapService;

        public ProfileDataService(IMapService mapService, IEmployeeRepository employees, IJobTitleRepository jobTitles, IAspNetUsersRepository users)
        {
            _employees = employees;
            _jobTitles = jobTitles;
            _mapService = mapService;
            _users = users;
        }

        public UserProfileDTO GetUserData(string userEmail)
        {
            string teamName = "No team";

            string teamLeaderName = "No team leader";

            var user = _users.GetByUserName(userEmail);

            var employee = _employees.GetById(user.Id);

            employee.JobTitle = _jobTitles.GetById(employee.JobTitleID);

            if (!employee.EmployeesTeam.Count.Equals(0))
            {
                var employeeTeam = employee.EmployeesTeam.First();

                teamName = employeeTeam.TeamName;

                var teamLeader = _employees.GetById(employeeTeam.TeamLeadID);

                teamLeaderName = teamLeader.Name + teamLeader.Surname;
            }

            UserProfileDTO userData = new UserProfileDTO
            {
                Name = employee.Name,
                Surname = employee.Surname,
                JobTitle = employee.JobTitle.JobTitleName,
                BirthDate = employee.BirthDate.Date,
                Status = employee.Status,
                PersonalMail = employee.PersonalMail,
                Email = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Skype = employee.Skype,
                HireDate = employee.HireDate.Date,
                TeamName = teamName,
                TeamLeader = teamLeaderName
            };

            return userData;
        }

        public List<VacationDTO> GetUserVacationsData(string userEmail)
        {
            var user = _users.GetByUserName(userEmail);

            var employee = _employees.GetById(user.Id);

            return _mapService.Map<ICollection<Vacation>, List<VacationDTO>>(employee.Vacations.ToList());
        }

        public VacationBalanceDTO GetUserVacationBalance(string userEmail)
        {
            var user = _users.GetByUserName(userEmail);

            var employee = _employees.GetById(user.Id);
          
            return new VacationBalanceDTO { Balance = employee.VacationBalance };
        }

        public EntityMapTo MapEntity<EntityToMapFrom, EntityMapTo>(EntityToMapFrom entity)
        { 
           return _mapService.Map<EntityToMapFrom, EntityMapTo >(entity);
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _employees.Dispose();
            _users.Dispose();
        }

    }
}
