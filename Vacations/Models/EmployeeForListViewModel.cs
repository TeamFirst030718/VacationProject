using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class EmployeeForListViewModel
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int VacationBalance { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkEmail { get; set; }
        public string CurrentVacationID { get; set; }
    }
}