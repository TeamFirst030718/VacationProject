﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VacationsBLL.DTOs;
using VacationsBLL.Enums;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL
{
    public class EmployeeService : IEmployeeService
    {
        private const string Empty = "None";
        private IEmployeeRepository _employees;
        private IJobTitleRepository _jobTitles;
        private ITeamRepository _teams;
        private IRolesRepository _roles;
        private IUsersRepository _users;
        private ITransactionTypeRepository _transactionTypes;
        private ITransactionRepository _transactions;

        public EmployeeService(IEmployeeRepository employees,
                               IJobTitleRepository jobTitles,
                               ITeamRepository teams,
                               IUsersRepository users,
                               IRolesRepository roles,
                               ITransactionTypeRepository transactionTypes,
                               ITransactionRepository transactions)
        {
            _employees = employees;
            _jobTitles = jobTitles;
            _teams = teams;
            _roles = roles;
            _users = users;
            _transactionTypes = transactionTypes;
            _transactions = transactions;
        }

        public void Create(EmployeeDTO employee)
        {
            _employees.Add(Mapper.Map<EmployeeDTO, Employee>(employee));
        }

        public EmployeeDTO GetUserById(string id)
        {
            return Mapper.Map<Employee, EmployeeDTO>(_employees.GetById(id));
        }

        public JobTitleDTO GetJobTitleById(string id)
        {
            return Mapper.Map<JobTitle,JobTitleDTO>(_jobTitles.GetById(id));
        }

        public string GetRoleByUserId(string id)
        {
            return _roles.GetById(_users.GetById(id).AspNetRoles.First().Id).Name;
        }

        public string GetStatusByEmployeeId(string id)
        {
            return _employees.GetById(id).Status.Equals(true) ? "Active" : "Fired";
        }

        public List<JobTitleDTO> GetJobTitles()
        {
            var jobTitles = _jobTitles.Get();

            return jobTitles.Select(jobTitle => Mapper.Map<JobTitle, JobTitleDTO>(jobTitle)).ToList();
        }

        public string GetJobTitleIdByName(string jobTitleName)
        {
            return _jobTitles.Get().FirstOrDefault(x => x.JobTitleName.Equals(jobTitleName)).JobTitleID;
        }

        public void UpdateEmployee(EmployeeDTO employee)
        {
            var employeeToUpdate = _employees.GetById(employee.EmployeeID);

            MapChanges(employeeToUpdate, employee);

            _employees.Update();
        }

        public void UpdateEmployeeBalance(EmployeeDTO employee, string comment)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var employeeToUpdate = _employees.GetById(employee.EmployeeID);

                var transaction = new VacationsDAL.Entities.Transaction
                {
                    BalanceChange = employee.VacationBalance-employeeToUpdate.VacationBalance,
                    Discription = comment,
                    EmployeeID = employeeToUpdate.EmployeeID,
                    TransactionDate = DateTime.UtcNow,
                    TransactionTypeID = _transactionTypes.GetByType(TransactionTypeEnum.ForceBalanceChangeTransaction.ToString()).TransactionTypeID,
                    TransactionID = Guid.NewGuid().ToString()
                };

                employeeToUpdate.VacationBalance = employee.VacationBalance;

                _employees.Update();

                _transactions.Add(transaction);

                scope.Complete();
            }      
        }

        public BalanceChangeDTO GetEmployeeDataForBalanceChange(string id)
        {
            var employee = _employees.GetById(id);
            var jobTitle = _jobTitles.GetById(employee.JobTitleID).JobTitleName;
            var employeeData = new BalanceChangeDTO
            {
                EmployeeID = employee.EmployeeID,
                EmployeeName = $"{employee.Name} {employee.Surname}",
                JobTitle = jobTitle,
                TeamLeadName = employee.EmployeesTeam.Count.Equals(0) ? Empty : _employees.GetById(employee.EmployeesTeam.First().TeamLeadID).Name,
                TeamName = employee.EmployeesTeam.Count.Equals(0) ? Empty : employee.EmployeesTeam.First().TeamName,
                Balance = employee.VacationBalance                
            };
            return employeeData;
        }
      
        public string GetTeamNameById(string id)
        {
            return _teams.GetById(id).TeamName;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return Mapper.MapCollection<Employee, EmployeeDTO>(_employees.Get());
        }

        public void AddToTeam(string EmployeeID, string TeamID)
        {
            var team = _teams.GetById(TeamID);
            var employee = _employees.GetById(EmployeeID);
            _teams.AddEmployee(EmployeeID, TeamID);
        }

        public void RemoveFromTeam(string EmployeeID, string TeamID)
        {
            var team = _teams.GetById(TeamID);
            var employee = _employees.GetById(EmployeeID);
            _teams.RemoveEmployee(EmployeeID, TeamID);
        }

        public IEnumerable<EmployeeDTO> GetAllFreeEmployees()
        {
            return Mapper.MapCollection<Employee, EmployeeDTO>(_employees.Get(x => x.EmployeesTeam.Count == 0).ToArray());
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByTeamId(string id)
        {
            var result = new List<EmployeeDTO>();

            var employees = _employees.Get().ToList();

            foreach (var employee in employees)
            {
                foreach (var team in employee.EmployeesTeam)
                {
                    if (team.TeamID == id)
                    {
                        result.Add(Mapper.Map<Employee, EmployeeDTO>(employee));
                        break;
                    }
                }
            }

            return result;
        }

        private void MapChanges(Employee entity, EmployeeDTO changes)
        {
            var entityChanges = Mapper.Map<EmployeeDTO, Employee>(changes);

            entity.BirthDate = entityChanges.BirthDate;
            entity.DateOfDismissal = entityChanges.DateOfDismissal;
            entity.EmployeeID = entityChanges.EmployeeID;
            entity.HireDate = entityChanges.HireDate;
            entity.JobTitleID = entityChanges.JobTitleID;
            entity.Name = entityChanges.Name;
            entity.PersonalMail = entityChanges.PersonalMail;
            entity.PhoneNumber = entityChanges.PhoneNumber;
            entity.Skype = entityChanges.Skype;
            entity.Status = entityChanges.Status;
            entity.Surname = entityChanges.Surname;
            entity.WorkEmail = entityChanges.WorkEmail;
        }
    }
}
