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
    public  class ProfileDataService :  IProfileDataService
    {
        private IEmployeeRepository _employees;

        private IJobTitleRepository _jobTitles;

        private IAspNetUsersRepository _users;

        private IVacationRepository _vacations;

        private IVacationStatusTypeRepository _vacationStatusTypes;

        private IVacationTypeRepository _vacationTypes;

        private IMapService _mapService;

        public ProfileDataService(IMapService mapService, 
                           IEmployeeRepository employees,
                           IJobTitleRepository jobTitles,
                           IAspNetUsersRepository users, 
                           IVacationRepository vacations,
                           IVacationStatusTypeRepository vacationStatusTypes,
                           IVacationTypeRepository vacationTypes)
        {
            _employees = employees;
            _jobTitles = jobTitles;
            _mapService = mapService;
            _vacations = vacations;
            _vacationTypes = vacationTypes;
            _vacationStatusTypes = vacationStatusTypes;
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

        public List<ProfileVacationDTO> GetUserVacationsData(string userEmail)
        {
            var employee = _employees.GetByEmail(userEmail);

            var vacations = _vacations.GetAll().Where(x => x.EmployeeID.Equals(employee.EmployeeID)).Select(x => new ProfileVacationDTO
            {
                VacationType = _vacationTypes.GetById(x.VacationTypeID).VacationTypeName,
                VacationStatusType = _vacationStatusTypes.GetById(x.VacationStatusTypeID).VacationStatusName,
                Comment = x.Comment,
                DateOfBegin = x.DateOfBegin,
                DateOfEnd = x.DateOfEnd,
                Duration = x.Duration
            }).ToList();

            return vacations;/* vacations.ToList();*/
        }

        public VacationBalanceDTO GetUserVacationBalance(string userEmail)
        {
            var employee = _employees.GetByEmail(userEmail);
          
            return new VacationBalanceDTO { Balance = employee.VacationBalance };
        }

        public EntityMapTo MapEntity<EntityToMapFrom, EntityMapTo>(EntityToMapFrom entity)
        { 
            var a = _mapService.Map<EntityToMapFrom, EntityMapTo>(entity);
            return _mapService.Map<EntityToMapFrom, EntityMapTo >(entity);
        }

        public IEnumerable<EntityMapTo> MapCollection<EntityToMapFrom, EntityMapTo>(IEnumerable<EntityToMapFrom> entityCollection)
        {
            return _mapService.MapCollection<EntityToMapFrom, EntityMapTo>(entityCollection);
        }

    }
}
