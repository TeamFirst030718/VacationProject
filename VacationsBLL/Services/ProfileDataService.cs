using System.Linq;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public  class ProfileDataService :  IProfileDataService
    {
        private IEmployeeRepository _employees;

        private IJobTitleRepository _jobTitles;

        private IUsersRepository _users;

        private IVacationRepository _vacations;

        private IVacationStatusTypeRepository _vacationStatusTypes;

        private IVacationTypeRepository _vacationTypes;


        public ProfileDataService(IEmployeeRepository employees,
                                  IJobTitleRepository jobTitles,
                                  IUsersRepository users, 
                                  IVacationRepository vacations,
                                  IVacationStatusTypeRepository vacationStatusTypes,
                                  IVacationTypeRepository vacationTypes)
        {
            _employees = employees;
            _jobTitles = jobTitles;
            _vacations = vacations;
            _vacationTypes = vacationTypes;
            _vacationStatusTypes = vacationStatusTypes;
            _users = users;
        }

        public ProfileDTO GetUserData(string userEmail)
        {

            var employee = _employees.GetByEmail(userEmail);

            var jobTitles = _jobTitles.Get();

            if (employee != null)
            {
                var userData = Mapper.Map<Employee, ProfileDTO>(employee);

                userData.TeamName = employee.EmployeesTeam.Count.Equals(0) ? "None" : employee.EmployeesTeam.First().TeamName;

                userData.TeamLeader = employee.EmployeesTeam.Count.Equals(0) ? "None" : string.Format($"{ _employees.Get(x => x.EmployeeID.Equals(employee.EmployeesTeam.First().TeamLeadID)).First().Name}" +
                                                                                                      $"{ _employees.Get(x => x.EmployeeID.Equals(employee.EmployeesTeam.First().TeamLeadID)).First().Surname}");

                userData.JobTitle = jobTitles.FirstOrDefault(x => x.JobTitleID.Equals(employee.JobTitleID)).JobTitleName;

                return userData;
            }

            return null;         
        }

        public ProfileVacationDTO[] GetUserVacationsData(string userEmail)
        {
            var employee = _employees.GetByEmail(userEmail);

            var vacations = _vacations.Get(x => x.EmployeeID.Equals(employee.EmployeeID)).Select(x => new ProfileVacationDTO
            {
                VacationType = _vacationTypes.GetById(x.VacationTypeID).VacationTypeName,
                VacationStatusType = _vacationStatusTypes.GetById(x.VacationStatusTypeID).VacationStatusName,
                Comment = x.Comment,
                DateOfBegin = x.DateOfBegin,
                DateOfEnd = x.DateOfEnd,
                Duration = x.Duration
            }).ToArray();

            return vacations;
        }

        public VacationBalanceDTO GetUserVacationBalance(string userEmail)
        {
            var employee = _employees.GetByEmail(userEmail);
          
            return new VacationBalanceDTO { Balance = employee.VacationBalance };
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _employees.Dispose();
            _users.Dispose();
        }

    }
}
