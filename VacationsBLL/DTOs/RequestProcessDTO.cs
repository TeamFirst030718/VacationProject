using System;

namespace VacationsBLL.DTOs
{
    public class RequestProcessDTO
    {
        public string EmployeeID { get; set; }
        public string VacationID { get; set; }
        public string EmployeeName { get; set; }
        public string JobTitle { get; set; }
        public string TeamName { get; set; }
        public string TeamLeadName { get; set; }
        public string VacationType { get; set; }
        public DateTime DateOfBegin { get; set; }
        public DateTime DateOfEnd { get; set; }
        public string Comment { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public string ProcessedBy { get; set; }
        public int EmployeesBalance{ get; set; }
    }
}
