using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Vacations.Models
{
    public class EmployeeViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "work mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "surname")]
        public string Surname { get; set; }

        [Display(Name = "date of birth")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "personal mail")]
        public string PersonalMail { get; set; }

        [Display(Name = "skype")]
        public string Skype { get; set; }

        [Display(Name = "hire date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "status")]
        public bool Status { get; set; }

        [Display(Name = "date of dismissal")]
        public DateTime? DateOfDismissal { get; set; }

        [Display(Name = "days in vacation")]
        public int VacationBalance { get; set; }

        [Display(Name = "job title")]
        public string JobTitleID { get; set; }

    }
}
