using System;
using System.ComponentModel.DataAnnotations;

namespace Vacations.Models
{
    public class ProfileViewModel
    {
        public string EmployeeID { get; set; }

        [Display(Name = "work mail")]
        public string WorkEmail { get; set; }

        [Display(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "surname")]
        public string Surname { get; set; }

        [Display(Name = "birth date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "personal mail")]     
        public string PersonalMail { get; set; }

        [Display(Name = "skype")]
        public string Skype { get; set; }

        [Display(Name = "phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "hire date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "status")]
        public bool Status { get; set; }

        [Display(Name = "date of dismissal")]
        public DateTime? DateOfDismissal { get; set; }

        [Display(Name = "balance")]
        public int VacationBalance { get; set; }

        [Display(Name = "job title")]
        public string JobTitle { get; set; }

        [Display(Name = "team name")]
        public string TeamName { get; set; }

        [Display(Name = "team leader")]
        public string TeamLeader { get; set; }
    }
}