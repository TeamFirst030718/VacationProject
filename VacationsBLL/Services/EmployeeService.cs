using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Models;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;
using VacationsDAL.Unit;

namespace VacationsBLL
{
    public class EmployeeService : IEmployeeService
    {

        private IEmployeeUnitOfWork _employeesData { get; set; }

        public EmployeeService(IEmployeeUnitOfWork repository)
        {
            _employeesData = repository;
        }

        public void Create(EmployeeDTO employee)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>()).CreateMapper();

            var _employee = mapper.Map<EmployeeDTO, Employee>(employee);

            _employeesData.Employees.Add(_employee);
        }
    }
}
