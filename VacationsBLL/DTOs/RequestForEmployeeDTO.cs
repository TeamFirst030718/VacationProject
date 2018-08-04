using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationsBLL.DTOs
{
    public class RequestForEmployeeDTO
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string JobTitle { get; set; }
        public string TeamName { get; set; }
        public string TeamLeadName { get; set; }
    }
}
