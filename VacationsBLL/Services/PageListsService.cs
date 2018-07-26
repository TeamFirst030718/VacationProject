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

        public PageListsService(IJobTitleRepository jobTitles, IRolesRepository roles, IVacationTypeRepository types)
        {
            _jobTitles = jobTitles;

            _roles = roles;

            _vacationTypes = types;
        }

        public SelectListItem[] SelectEditRoles(string value)
        {

            var aspNetRolesList = _roles.Get();

            var tt = aspNetRolesList.Select(aspNetRole => new SelectListItem
            {
                Text = aspNetRole.Name,
                Value = aspNetRole.Name,
                Selected = aspNetRole.Name == value
            }).ToArray();

            return tt;
        }

        public List<SelectListItem> SelectRoles()
        {
           
            var aspNetRolesList = _roles.Get();

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

        public List<SelectListItem> SelectJobTitles()
        {
            var jobTitlesList = _jobTitles.Get();

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

        public List<SelectListItem> SelectEditJobTitles(string value)
        {
            var jobTitlesList = _jobTitles.Get();

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

        public List<SelectListItem> SelectStatuses()
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

        public List<SelectListItem> SelectEditStatuses(string value)
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
        public SelectListItem[] SelectVacationTypes()
        {
            var vacationTypesList = _vacationTypes.Get();

            var vacationTypesSelectList = new List<SelectListItem>();

            foreach (var vacationType in vacationTypesList)
            {
                vacationTypesSelectList.Add(new SelectListItem
                {
                    Text = vacationType.VacationTypeName,
                    Value = vacationType.VacationTypeID
                });
            }

            return vacationTypesSelectList.ToArray();
        }

        public void Dispose()
        {
            _jobTitles.Dispose();
            _roles.Dispose();
        }
    }
}
