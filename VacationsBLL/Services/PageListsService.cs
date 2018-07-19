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

        public PageListsService(IJobTitleRepository jobTitles, IAspNetRolesRepository roles)
        {
            _jobTitles = jobTitles;

            _roles = roles;
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
                    Value = aspNetRole.Name
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

        public void Dispose()
        {
            _jobTitles.Dispose();
            _roles.Dispose();
        }
    }
}
