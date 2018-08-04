using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class EmployeeListViewModel
    {
        public EmployeeForListViewModel EmployeeDto { get; set; }
        public EmployeeListTeamViewModel TeamDto { get; set; }
    }
}