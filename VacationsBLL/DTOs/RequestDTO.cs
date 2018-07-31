using System;

namespace VacationsBLL.DTOs
{
    public class RequestDTO
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
        public string ProcessedBy { get; set; }
    }
}
