using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VacationsBLL.Interfaces
{
    public interface IPageListsService: IDisposable
    {
        List<SelectListItem> AspNetRolesSelectList();
        List<SelectListItem> JobTitlesSelectList();
        List<SelectListItem> StatusSelectList();
        List<SelectListItem> EmployeesList();
    }
}
