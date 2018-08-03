using System.Linq;
using VacationsBLL.DTOs;
using VacationsBLL.Functions;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public  class ProfileDataService :  IProfileDataService
    {
        private const string empty = "None";
        private IEmployeeRepository _employees;
        private IJobTitleRepository _jobTitles;
        private IVacationRepository _vacations;
        private IVacationStatusTypeRepository _vacationStatusTypes;
        private IVacationTypeRepository _vacationTypes;

        public ProfileDataService(IEmployeeRepository employees,
                                  IJobTitleRepository jobTitles,
                                  IVacationRepository vacations,
                                  IVacationStatusTypeRepository vacationStatusTypes,
                                  IVacationTypeRepository vacationTypes)
        {
            _employees = employees;
            _jobTitles = jobTitles;
            _vacations = vacations;
            _vacationTypes = vacationTypes;
            _vacationStatusTypes = vacationStatusTypes;
        }

        public ProfileDTO GetUserData(string id)
        {
            var employee = _employees.GetById(id);
            var teamLeader = empty;
            var jobTitle = _jobTitles.GetById(employee.JobTitleID).JobTitleName;

            if (employee.EmployeesTeam.Count > 0)
            {
               var tempLead = _employees.GetById(employee.EmployeesTeam.First().TeamLeadID);
               teamLeader = string.Format($"{tempLead.Name} {tempLead.Surname}");
            }
            if (employee != null)
            {
                var userData = Mapper.Map<Employee, ProfileDTO>(employee);
                userData.TeamName = employee.EmployeesTeam.Count.Equals(0) ? empty : employee.EmployeesTeam.First().TeamName;
                userData.TeamLeader = teamLeader;
                userData.JobTitle = jobTitle;
                userData.EmployeeID = employee.EmployeeID;
                return userData;
            }

            return null;         
        }

        public ProfileVacationDTO[] GetUserVacationsData(string id)
        {
            var employee = _employees.GetById(id);
            var vacationStatuses = _vacationStatusTypes.Get();
            var vacationTypes = _vacationTypes.Get();   

            var vacations = _vacations.Get(x => x.EmployeeID.Equals(employee.EmployeeID)).Select(x => new ProfileVacationDTO
            {
                VacationType = vacationTypes.FirstOrDefault(y=>y.VacationTypeID.Equals(x.VacationTypeID)).VacationTypeName,
                Comment = x.Comment,
                DateOfBegin = x.DateOfBegin,
                DateOfEnd = x.DateOfEnd,
                Duration = x.Duration,
                Status = vacationStatuses.FirstOrDefault(y => y.VacationStatusTypeID.Equals(x.VacationStatusTypeID)).VacationStatusName,
                Created = x.Created
            }).OrderBy(x=>FunctionHelper.VacationSortFunc(x.Status)).ThenBy(x=>x.Created).ToArray();

            return vacations;
        }

        public VacationBalanceDTO GetUserVacationBalance(string id)
        {
            var employee = _employees.GetById(id);

            return new VacationBalanceDTO { Balance = employee.VacationBalance };
        }
    }
}
