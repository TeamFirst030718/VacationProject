using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace VacationsBLL.Interfaces
{
    public interface IPageListsService : IDisposable
    {
        SelectListItem[] SelectEditRoles(string editValue);
        List<SelectListItem> SelectEditJobTitles(string editValue);
        List<SelectListItem> SelectEditStatuses(string editValue);
        List<SelectListItem> SelectRoles();
        List<SelectListItem> SelectJobTitles();
        List<SelectListItem> SelectStatuses();
        SelectListItem[] SelectVacationTypes();
    }
}
