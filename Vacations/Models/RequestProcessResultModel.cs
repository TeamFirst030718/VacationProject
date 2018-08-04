using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class RequestProcessResultModel
    {
        public string VacationID { get; set; }

        [Required(ErrorMessage = " required field")]
        [Display(Name = "duration")]
        [Range(1, 28, ErrorMessage = " should be from 1 to 28.")]
        public int Duration { get; set; }

        public string Discription { get; set; }

        public string EmployeeID { get; set; }

        public string Result { get; set; }

        [Required(ErrorMessage = " required field")]
        [Display(Name = "from")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBegin { get; set; }

        [Required(ErrorMessage = " required field")]
        [Display(Name = "to")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEnd { get; set; }
    }
}