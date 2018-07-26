using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.Services
{
    public class PageListsService : IPageListsService
    {
        IJobTitleRepository _jobTitles;

        IRolesRepository _roles;

        IVacationTypeRepository _vacationTypes;

        IEmployeeRepository _employees;

        public PageListsService(IJobTitleRepository jobTitles, IRolesRepository roles, IVacationTypeRepository types, IEmployeeRepository employees)
        {
            _jobTitles = jobTitles;

            _roles = roles;

            _vacationTypes = types;

            _employees = employees;
        }

        public SelectListItem[] EmployeesList()
        {
            var employeesList = _employees.Get().Select(emp=>new SelectListItem
            {
                Text = emp.Name + " " + emp.Surname,
                Value = emp.EmployeeID
            });

            return employeesList.ToArray();
        }

        public SelectListItem[] SelectEditRoles(string value)
        {
            var roles = _roles.Get().Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name,
                Selected = role.Name == value
            }).ToArray();

            return roles;
        }

        public SelectListItem[] SelectRoles()
        {
            var roles = _roles.Get().Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name,
            }).ToArray();

            return roles;
        }

        public SelectListItem[] SelectJobTitles()
        {
            var jobTitles = _jobTitles.Get().Select(jobTitle => new SelectListItem
            {
                Text = jobTitle.JobTitleName,
                Value = jobTitle.JobTitleID,
            }).ToArray();

            return jobTitles;
        }

        public SelectListItem[] SelectEditJobTitles(string value)
        {
            var jobTitles = _jobTitles.Get().Select(jobTitle => new SelectListItem
            {
                Text = jobTitle.JobTitleName,
                Value = jobTitle.JobTitleID,
                Selected = jobTitle.JobTitleID == value
            }).ToArray();

            return jobTitles;
        }

        public SelectListItem[] SelectStatuses()
        {
            var statusSelectList = new SelectListItem[]
            {
                new SelectListItem
                {
                    Text = "Active",
                    Value = "true"
                },

                new SelectListItem
                {
                    Text = "Fired",
                    Value = "false"
                }
            };

            return statusSelectList;
        }

        public SelectListItem[] SelectEditStatuses(string value)
        {
            var statusSelectList = new SelectListItem[]
            {
                new SelectListItem
                {
                    Text = "Active",
                    Value = "True",
                    Selected = "True" == value
                },

                new SelectListItem
                {
                    Text = "Fired",
                    Value = "False",
                    Selected = "False" == value
                }
            };

            return statusSelectList;
        }

        public SelectListItem[] SelectVacationTypes()
        {

            var vacationTypes = _vacationTypes.Get().Select(type => new SelectListItem
            {
                Text = type.VacationTypeName,
                Value = type.VacationTypeID
            }).ToArray();

            return vacationTypes;
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _employees.Dispose();
            _roles.Dispose();
            _vacationTypes.Dispose();
        }
    }
}
