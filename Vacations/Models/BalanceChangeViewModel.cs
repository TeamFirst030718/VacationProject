using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class BalanceChangeViewModel
    {
        [Required(ErrorMessage = " required field")]
        [Display(Name = "comment")]
        [StringLength(100, ErrorMessage = " should be shorter.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = " required field")]
        [Display(Name = "balance")]
        [Range(1, 28, ErrorMessage = " should be from 1 to 28.")]
        public int Balance { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string JobTitle { get; set; }

        public string TeamName { get; set; }

        public string TeamLeadName { get; set; } 
    }
}