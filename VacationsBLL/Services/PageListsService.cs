using System.Collections.Generic;
using System.Web.Mvc;
using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.Services
{
    public class PageListsService : IPageListsService
    {
        IJobTitleRepository _jobTitles;

        IAspNetRolesRepository _roles;

        IVacationTypeRepository _vacationTypes;

        public PageListsService(IJobTitleRepository jobTitles, IAspNetRolesRepository roles, IVacationTypeRepository types)
        {
            _jobTitles = jobTitles;

            _roles = roles;

            _vacationTypes = types;
        }

        public List<SelectListItem> AspNetRolesSelectList(string value)
        {

            var aspNetRolesList = _roles.GetAll();

            var aspNetRolesSelectList = new List<SelectListItem>();

            foreach (var aspNetRole in aspNetRolesList)
            {
                aspNetRolesSelectList.Add(new SelectListItem
                {
                    Text = aspNetRole.Name,
                    Value = aspNetRole.Name,
                    Selected = aspNetRole.Name == value
                });
            }

            return aspNetRolesSelectList;
        }

        public List<SelectListItem> AspNetRolesSelectList()
        {
           
            var aspNetRolesList = _roles.GetAll();

            var aspNetRolesSelectList = new List<SelectListItem>();

            foreach (var aspNetRole in aspNetRolesList)
            {
                aspNetRolesSelectList.Add(new SelectListItem
                {
                    Text = aspNetRole.Name,
                    Value = aspNetRole.Name,
                });
            }

            return aspNetRolesSelectList;
        }

        public List<SelectListItem> JobTitlesSelectList()
        {
            var jobTitlesList = _jobTitles.GetAll();

            var jobTitlesSelectList = new List<SelectListItem>();

            foreach (var jobTitle in jobTitlesList)
            {
                jobTitlesSelectList.Add(new SelectListItem
                {
                    Text = jobTitle.JobTitleName,
                    Value = jobTitle.JobTitleID
                });
            }

            return jobTitlesSelectList;
        }

        public List<SelectListItem> JobTitlesSelectList(string value)
        {
            var jobTitlesList = _jobTitles.GetAll();

            var jobTitlesSelectList = new List<SelectListItem>();

            foreach (var jobTitle in jobTitlesList)
            {
                jobTitlesSelectList.Add(new SelectListItem
                {
                    Text = jobTitle.JobTitleName,
                    Value = jobTitle.JobTitleID,
                    Selected = jobTitle.JobTitleID == value
                });
            }

            return jobTitlesSelectList;
        }

        public List<SelectListItem> StatusSelectList()
        {
            var statusSelectList = new List<SelectListItem>
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

        public List<SelectListItem> StatusSelectList(string value)
        {
            var statusSelectList = new List<SelectListItem>
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
        public List<SelectListItem> VacationTypesSelectList()
        {
            var vacationTypesList = _vacationTypes.GetAll();

            var vacationTypesSelectList = new List<SelectListItem>();

            foreach (var vacationType in vacationTypesList)
            {
                vacationTypesSelectList.Add(new SelectListItem
                {
                    Text = vacationType.VacationTypeName,
                    Value = vacationType.VacationTypeID
                });
            }

            return vacationTypesSelectList;
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _roles.Dispose();
        }
    }
}
