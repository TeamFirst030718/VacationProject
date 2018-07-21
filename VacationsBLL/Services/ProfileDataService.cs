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
            string teamName = "None";

            string teamLeaderName = "None";

            var employee = _employees.GetByEmail(userEmail);

            employee.JobTitle = _jobTitles.GetById(employee.JobTitleID);

            if (!employee.EmployeesTeam.Count.Equals(0))
            {
                var employeeTeam = employee.EmployeesTeam.First();

                teamName = employeeTeam.TeamName;

                var teamLeader = _employees.GetById(employeeTeam.TeamLeadID);

                teamLeaderName = teamLeader.Name + teamLeader.Surname;
            }

           var userData = _mapService.Map<Employee, UserProfileDTO>(employee);

            userData.TeamName = teamName;

            userData.TeamLeader = teamLeaderName;

            userData.JobTitle = _jobTitles.GetById(employee.JobTitleID).JobTitleName;

            return userData;
        }

        public List<VacationDTO> GetUserVacationsData(string userEmail)
        {
            var employee = _employees.GetByEmail(userEmail);

            return _mapService.Map<ICollection<Vacation>, List<VacationDTO>>(employee.Vacations.ToList());
        }

        public VacationBalanceDTO GetUserVacationBalance(string userEmail)
        {
            var employee = _employees.GetByEmail(userEmail);
          
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
