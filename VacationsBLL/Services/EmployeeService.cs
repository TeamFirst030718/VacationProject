using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using VacationsBLL.DTOs;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(EmployeeDTO employee)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>()).CreateMapper();

            _unitOfWork.Employees.Add(mapper.Map<EmployeeDTO, Employee>(employee));
        }

        public EmployeeDTO GetUserById(string id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();

            var employee = _unitOfWork.Employees.GetById(id);

            return mapper.Map<Employee, EmployeeDTO>(employee);
        }

        public List<JobTitleDTO> GetJobTitles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobTitle, JobTitleDTO>()).CreateMapper();

            var jobTitles = _unitOfWork.JobTitles.GetAll();

            return jobTitles.Select(jobTitle => mapper.Map<JobTitle, JobTitleDTO>(jobTitle)).ToList();
        }

        public string GetJobTitleIdByName(string jobTitleName)
        {
            return _unitOfWork.JobTitles.GetAll().FirstOrDefault(x => x.JobTitleName.Equals(jobTitleName)).JobTitleID;
        }

        public void SaveChanges()
        {
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
