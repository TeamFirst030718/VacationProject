using System;
using System.ComponentModel.DataAnnotations;

namespace Vacations.Models
{
    public class ProfileViewModel
    {
        public string EmployeeID { get; set; }

        public string WorkEmail { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public string PersonalMail { get; set; }

        public string Skype { get; set; }

        public string PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime HireDate { get; set; }

        public bool Status { get; set; }

        public DateTime? DateOfDismissal { get; set; }

        public int VacationBalance { get; set; }

        public string JobTitle { get; set; }

        public string TeamName { get; set; }

        public string TeamLeader { get; set; }
    }
}