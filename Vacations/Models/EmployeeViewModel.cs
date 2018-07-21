﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Vacations.Models
{
    public class EmployeeViewModel
    {
        public string EmployeeID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "work email")]
        public string WorkEmail { get; set; }

        [Display(Name = "name")]
        public string Name { get; set; }

        [Display(Name = "surname")]
        public string Surname { get; set; }

        [Display(Name = "date of birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "personal email")]
        public string PersonalMail { get; set; }

        [Display(Name = "phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "skype")]
        public string Skype { get; set; }

        [Display(Name = "hire date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Display(Name = "status")]
        public bool Status { get; set; }
        
        [Display(Name = "date of dismissal")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfDismissal { get; set; }

        [Display(Name = "days in vacation")]
        public int VacationBalance { get; set; }

        [Display(Name = "job title")]
        public string JobTitleID { get; set; }

    }
}
