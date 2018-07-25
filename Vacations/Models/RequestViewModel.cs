using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations.Models
{
    public class RequestViewModel
    {
        public string EmployeeID { get; set; }
        public string VacationID { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string VacationDates { get; set; }
        public int Duration { get; set; }
        public int EmployeesBalance { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}
