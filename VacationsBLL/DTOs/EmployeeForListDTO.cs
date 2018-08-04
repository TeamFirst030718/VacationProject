using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationsBLL.DTOs
{
    public class EmployeeForListDTO
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
