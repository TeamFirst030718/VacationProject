using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Vacations.Models
{
    public class EmployeeViewModel
    {
        public string EmployeeID { get; set; }

        [Required(ErrorMessage =" required field")]
        [EmailAddress(ErrorMessage =" invalid email")]
        [Display(Name = "work email")]
        [Remote("ValidateEmail", "RemoteValidation",AdditionalFields = "EmployeeID")]
        public string WorkEmail { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "date of birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        
        [Required(ErrorMessage =" required field")]
        [EmailAddress(ErrorMessage = " invalid email")]
        [Display(Name = "personal email")]
        public string PersonalMail { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "skype")]
        public string Skype { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "hire date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "status")]
        public bool Status { get; set; }
        
        [Display(Name = "date of dismissal")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfDismissal { get; set; }

       
        [Required(ErrorMessage =" required field")] 

        [Display(Name = "days in vacation")]
        [Range(1,28, ErrorMessage = "Duration should be positive number.")]
        [Remote("ValidateBalance", "RemoteValidation")]
        public int VacationBalance { get; set; }

        [Display(Name = "job title")]
        public string JobTitleID { get; set; }
    }
}
