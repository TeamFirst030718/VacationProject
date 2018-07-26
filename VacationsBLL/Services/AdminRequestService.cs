using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.DTOs;
using VacationsBLL.Enums;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
namespace VacationsBLL.Services
{
    public class AdminRequestService : IAdminRequestService
    {
        public string AdminID { get; set; }
        private const string empty = "None";
        private IUserService _userService;
        private IJobTitleRepository _jobTitles;
        private IEmployeeRepository _employees;
        private IVacationRepository _vacations;
        private ITransactionRepository _transactions;
        private IVacationTypeRepository _vacationTypes;
        private IVacationStatusTypeRepository _vacationStatusTypes;
        private ITransactionTypeRepository _transactionTypes;
        
        public AdminRequestService(string id)
        {
            AdminID = id;
        }

        public AdminRequestService(ITransactionTypeRepository transactionTypes,
                                   ITransactionRepository transactions,
                                   IJobTitleRepository jobTitles,
                                   IEmployeeRepository employees,
                                   IVacationRepository vacations,
                                   IUserService userService,
                                   IVacationStatusTypeRepository vacationStatusTypes,
                                   IVacationTypeRepository vacationTypes)
        {
            _jobTitles = jobTitles;
            _employees = employees;
            _vacations = vacations;
            _userService = userService;
            _vacationStatusTypes = vacationStatusTypes;
            _vacationTypes = vacationTypes;
            _transactions = transactions;
            _transactionTypes = transactionTypes;
        }
        public void SetAdminID(string adminEmail)
        {
            AdminID = _employees.GetByEmail(adminEmail).EmployeeID;
        }

        private int VacationSortFunc(string statusType)
        {
            if (statusType.Equals(VacationStatusTypeEnum.Pending.ToString()))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public RequestDTO[] GetVacations()
        {
            IEnumerable<Employee> employees = _employees.Get();

            var vacations = _vacations.Get();

            var users = _userService.GetUsers();

            var vacationStatusTypes = _vacationStatusTypes.Get();

            Func<Employee, bool> whereLinq = emp => users.FirstOrDefault(u => u.Id.Equals(emp.EmployeeID)).AspNetRoles.First().Equals(RoleEnum.Administrator.ToString()) ||
                                              (emp.EmployeesTeam.Count.Equals(1) && emp.EmployeesTeam.First().TeamLeadID.Equals(users.FirstOrDefault(x => x.Email.Equals(AdminID)))) ||
                                              (emp.EmployeesTeam.Count.Equals(0));
            
            employees = _employees.Get(whereLinq);

            var requestsList = vacations.Where(vac => employees.Any(emp => emp.EmployeeID.Equals(vac.EmployeeID))).
                                  Join(employees, vac => vac.EmployeeID, emp => emp.EmployeeID, (vac, emp) => new RequestDTO
                                  {
                                      EmployeeID = emp.EmployeeID,
                                      VacationID = vac.VacationID,
                                      Name = string.Format($"{emp.Name} {emp.Surname}"),
                                      TeamName = emp.EmployeesTeam.Count.Equals(0) ? empty : emp.EmployeesTeam.First().TeamName,
                                      Duration = vac.Duration,
                                      VacationDates = string.Format($"{vac.DateOfBegin.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}-{vac.DateOfEnd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}"),
                                      EmployeesBalance = emp.VacationBalance,
                                      Created = vac.Created,
                                      Status = vacationStatusTypes.FirstOrDefault(type => type.VacationStatusTypeID.Equals(vac.VacationStatusTypeID)).VacationStatusName
                                  }).OrderBy(req=> VacationSortFunc(req.Status)).ThenByDescending(req=> req.Created).ToArray();

            return requestsList;
        }

        public RequestProcessDTO GetRequestDataById(string id)
        {
            var vacation = _vacations.GetById(id);
            if (vacation != null)
            {

                var employee = _employees.GetById(vacation.EmployeeID);
                var vacationType = _vacationTypes.GetById(vacation.VacationTypeID).VacationTypeName;
                var jobTitle = _jobTitles.GetById(employee.JobTitleID).JobTitleName;
                var status = _vacationStatusTypes.GetById(vacation.VacationStatusTypeID).VacationStatusName;
                var request = new RequestProcessDTO
                {
                    EmployeeID=employee.EmployeeID,
                    VacationID = vacation.VacationID,
                    Comment = vacation.Comment,
                    DateOfBegin = vacation.DateOfBegin,
                    DateOfEnd = vacation.DateOfEnd,
                    Duration = vacation.Duration,
                    EmployeeName = string.Format($"{employee.Name} {employee.Surname}"),
                    JobTitle = jobTitle,
                    Status = status,
                    VacationType = vacationType,
                    TeamLeadName = employee.EmployeesTeam.Count.Equals(0) ? empty : _employees.GetById(employee.EmployeesTeam.First().TeamLeadID).Name + _employees.GetById(employee.EmployeesTeam.First().TeamLeadID).Surname,
                    TeamName = employee.EmployeesTeam.Count.Equals(0) ? empty : employee.EmployeesTeam.First().TeamName
                };

                return request;
            }
            else
            {
                return new RequestProcessDTO();
            }
        }

        public void ApproveVacation(RequestProcessResultDTO result)
        {
           var vacation = _vacations.GetById(result.VacationID);
           var employee = _employees.GetById(result.EmployeeID);
            if (vacation != null && employee != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    vacation.VacationStatusTypeID = _vacationStatusTypes.GetByType(VacationStatusTypeEnum.Approved.ToString()).VacationStatusTypeID;
                    vacation.Duration = result.BalanceChange;
                    vacation.DateOfBegin = result.DateOfBegin;
                    vacation.DateOfEnd = result.DateOfEnd;
                    _vacations.Update(vacation);

                    employee.VacationBalance -= result.BalanceChange;

                    //_employees.Update(employee);

                    var transaction = new VacationsDAL.Entities.Transaction
                    {
                        BalanceChange = result.BalanceChange,
                        Discription = result.Discription ?? empty,
                        EmployeeID = result.EmployeeID,
                        TransactionDate = DateTime.UtcNow,
                        TransactionTypeID = _transactionTypes.GetByType(TransactionTypeEnum.VacationTransaction.ToString()).TransactionTypeID,
                        TransactionID = Guid.NewGuid().ToString(),
                    };
                    _transactions.Add(transaction);

                    scope.Complete();
                }    
            }                   
        }

        public void DenyVacation(RequestProcessResultDTO result)
        {
            var vacation = _vacations.GetById(result.VacationID);
            if (vacation != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    vacation.VacationStatusTypeID = _vacationStatusTypes.GetByType(VacationStatusTypeEnum.Denied.ToString()).VacationStatusTypeID;

                    _vacations.Update(vacation);

                    scope.Complete();
                }                         
            }        
        }
    }
}
