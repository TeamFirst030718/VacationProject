using System;
using System.Collections.Generic;

namespace VacationsBLL.DTOs
{
    public class EmployeeDTO
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalMail { get; set; }
        public string Skype { get; set; }
        public DateTime HireDate { get; set; }
        public bool Status { get; set; }
        public DateTime? DateOfDismissal { get; set; }
        public int VacationBalance { get; set; }
        public string JobTitleID { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkEmail { get; set; }
        public ICollection<TeamDTO> EmployeesTeam { get; set; }
    }
}
