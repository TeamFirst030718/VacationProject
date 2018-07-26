using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace VacationsBLL.Interfaces
{
    public interface IPageListsService: IDisposable
    {

        SelectListItem[] SelectEditRoles(string editValue);
        SelectListItem[] SelectEditJobTitles(string editValue);
        SelectListItem[] SelectEditStatuses(string editValue);
        SelectListItem[] SelectRoles();
        SelectListItem[] SelectJobTitles();
        SelectListItem[] SelectStatuses();
        SelectListItem[] SelectVacationTypes();
        SelectListItem[] EmployeesList();

    }
}
