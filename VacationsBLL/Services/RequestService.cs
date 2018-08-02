using System;
using System.Globalization;
using System.Linq;
using System.Transactions;
using VacationsBLL.DTOs;
using VacationsBLL.Enums;
using VacationsBLL.Functions;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
namespace VacationsBLL.Services
{
    public class RequestService : IRequestService
    {
        public string ReviewerID { get; set; }
        private const string Empty = "None";
        private IUsersRepository _users;
        private IJobTitleRepository _jobTitles;
        private IEmployeeRepository _employees;
        private IVacationRepository _vacations;
        private ITransactionRepository _transactions;
        private IVacationTypeRepository _vacationTypes;
        private IVacationStatusTypeRepository _vacationStatusTypes;
        private ITransactionTypeRepository _transactionTypes;
        private IEmailSendService _emailService;

        public RequestService(ITransactionTypeRepository transactionTypes,
                                   ITransactionRepository transactions,
                                   IJobTitleRepository jobTitles,
                                   IEmployeeRepository employees,
                                   IVacationRepository vacations,
                                   IVacationStatusTypeRepository vacationStatusTypes,
                                   IVacationTypeRepository vacationTypes,
                                   IUsersRepository users, IEmailSendService sendService)
        {
            _jobTitles = jobTitles;
            _employees = employees;
            _vacations = vacations;
            _vacationStatusTypes = vacationStatusTypes;
            _vacationTypes = vacationTypes;
            _transactions = transactions;
            _transactionTypes = transactionTypes;
            _users = users;
            _emailService = sendService;
        }

        public void SetReviewerID(string id)
        {
            ReviewerID = id;
        }

        public RequestDTO[] GetRequestsForAdmin(string searchKey=null)
        {
            var vacationStatusTypes = _vacationStatusTypes.Get();

            bool whereLinq(Employee emp) => _users.GetById(emp.EmployeeID).AspNetRoles.Any(role => role.Name.Equals(RoleEnum.Administrator.ToString())) ||
                                                    (emp.EmployeesTeam.Count.Equals(1) &&
                                                    emp.EmployeesTeam.First().TeamLeadID.Equals(ReviewerID)) ||
                                                    emp.EmployeesTeam.Count.Equals(0);
            Employee[] employees = _employees.Get(whereLinq);

            if (employees != null)
            {
                var requestsList = _vacations.Get().Join(employees, vac => vac.EmployeeID, emp => emp.EmployeeID, (vac, emp) => new RequestDTO
                {
                    EmployeeID = emp.EmployeeID,
                    VacationID = vac.VacationID,
                    Name = string.Format($"{emp.Name} {emp.Surname}"),
                    TeamName = emp.EmployeesTeam.Count.Equals(0) ? Empty : emp.EmployeesTeam.First().TeamName,
                    Duration = vac.Duration,
                    VacationDates = string.Format($"{vac.DateOfBegin.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}-{vac.DateOfEnd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}"),
                    EmployeesBalance = emp.VacationBalance,
                    Created = vac.Created,
                    Status = vacationStatusTypes.FirstOrDefault(type => type.VacationStatusTypeID.Equals(vac.VacationStatusTypeID)).VacationStatusName,                               
                }).OrderBy(req => FunctionHelper.VacationSortFunc(req.Status)).ThenBy(req => req.Created).ToArray();

                if(searchKey != null)
                {
                    requestsList = requestsList.Where(x => x.Name.ToLower().Contains(searchKey.ToLower())).ToArray();
                }

                return requestsList;
            }

            return new RequestDTO[0];

        }

        public RequestDTO[] GetRequestsForTeamLeader(string searchKey = null)
        {
            var vacationStatusTypes = _vacationStatusTypes.Get();

            bool whereLinq(Employee emp) => emp.EmployeesTeam.Count.Equals(1) && emp.EmployeesTeam.First().TeamLeadID.Equals(ReviewerID);

            Employee[] employees = _employees.Get(whereLinq);

            if (employees != null)
            {
                var requestsList = _vacations.Get().Join(employees, vac => vac.EmployeeID, emp => emp.EmployeeID, (vac, emp) => new RequestDTO
                {
                    EmployeeID = emp.EmployeeID,
                    VacationID = vac.VacationID,
                    Name = string.Format($"{emp.Name} {emp.Surname}"),
                    TeamName = emp.EmployeesTeam.Count.Equals(0) ? Empty : emp.EmployeesTeam.First().TeamName,
                    Duration = vac.Duration,
                    VacationDates = string.Format($"{vac.DateOfBegin.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}-{vac.DateOfEnd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}"),
                    EmployeesBalance = emp.VacationBalance,
                    Created = vac.Created,
                    Status = vacationStatusTypes.FirstOrDefault(type => type.VacationStatusTypeID.Equals(vac.VacationStatusTypeID)).VacationStatusName,
                }).OrderBy(req => FunctionHelper.VacationSortFunc(req.Status)).ThenBy(req => req.Created).ToArray();

                if (searchKey != null)
                {
                    requestsList = requestsList.Where(x => x.Name.ToLower().Contains(searchKey.ToLower())).ToArray();
                }

                return requestsList;
            }

            return new RequestDTO[0];
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
                string processedBy = null;

                if(vacation.ProcessedByID != null)
                {
                    var processedByTemp = _employees.GetById(vacation.ProcessedByID);
                    processedBy = string.Format($"{processedByTemp.Name} {processedByTemp.Surname}");
                }

                var request = new RequestProcessDTO
                {
                    EmployeeID = employee.EmployeeID,
                    VacationID = vacation.VacationID,
                    Comment = vacation.Comment,
                    DateOfBegin = vacation.DateOfBegin,
                    DateOfEnd = vacation.DateOfEnd,
                    Duration = vacation.Duration,
                    EmployeeName = string.Format($"{employee.Name} {employee.Surname}"),
                    JobTitle = jobTitle,
                    Status = status,
                    VacationType = vacationType,
                    TeamLeadName = employee.EmployeesTeam.Count.Equals(0) ? Empty : _employees.GetById(employee.EmployeesTeam.First().TeamLeadID).Name,
                    TeamName = employee.EmployeesTeam.Count.Equals(0) ? Empty : employee.EmployeesTeam.First().TeamName,
                    ProcessedBy = processedBy
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
                    var transaction = new VacationsDAL.Entities.Transaction
                    {
                        BalanceChange = result.BalanceChange,
                        Discription = result.Discription ?? Empty,
                        EmployeeID = result.EmployeeID,
                        TransactionDate = DateTime.UtcNow,
                        TransactionTypeID = _transactionTypes.GetByType(TransactionTypeEnum.VacationTransaction.ToString()).TransactionTypeID,
                        TransactionID = Guid.NewGuid().ToString()
                    };
                    _transactions.Add(transaction);

                    vacation.Duration = result.BalanceChange;
                    vacation.DateOfBegin = result.DateOfBegin;
                    vacation.DateOfEnd = result.DateOfEnd;
                    vacation.ProcessedByID = ReviewerID;
                    vacation.TransactionID = transaction.TransactionID;
                    vacation.VacationStatusTypeID = _vacationStatusTypes.GetByType(VacationStatusTypeEnum.Approved.ToString()).VacationStatusTypeID;
                    _vacations.Update(vacation);

                    employee.VacationBalance -= result.BalanceChange;
                    _employees.Update(employee);

                    _emailService.SendAsync(employee.WorkEmail, $"{employee.Name} {employee.Surname}", "Vacation request.", "Your vacation request was approved.",
                     $"{employee.Name} {employee.Surname}, your vacation request from {vacation.DateOfBegin.ToString("dd-MM-yyyy")} to {vacation.DateOfEnd.ToString("dd-MM-yyyy")} was approved. Have a nice vacation.");

                    scope.Complete();
                }
            }
        }

        public void DenyVacation(RequestProcessResultDTO result)
        {
            var vacation = _vacations.GetById(result.VacationID);
            var employee = _employees.GetById(result.EmployeeID);
            if (vacation != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    vacation.VacationStatusTypeID = _vacationStatusTypes.GetByType(VacationStatusTypeEnum.Denied.ToString()).VacationStatusTypeID;

                    vacation.ProcessedByID = ReviewerID;

                    _vacations.Update(vacation);

                    _emailService.SendAsync(employee.WorkEmail, $"{employee.Name} {employee.Surname}", "Vacation request.", "Your vacation request was denied.",
                           $"{employee.Name} {employee.Surname}, your vacation request from {vacation.DateOfBegin.ToString("dd-MM-yyyy")} to {vacation.DateOfEnd.ToString("dd-MM-yyyy")} was denied. Please, check it.");

                    scope.Complete();
                }
            }
        }
    }
}
