using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;
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

            _employeesData.Employees.Add(mapper.Map<EmployeeDTO, Employee>(employee));

            _employeesData.Save();
        }
        /*Check this method*/
        public EmployeeDTO GetUserById(string id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee,EmployeeDTO>()).CreateMapper();

            var employee = _employeesData.Employees.GetById(id);
         
            return mapper.Map<Employee, EmployeeDTO>(employee);
        }

        public  void Dispose()
        {
            _employeesData.Dispose();
        }
    }
}
