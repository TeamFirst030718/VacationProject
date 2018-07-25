using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class RequestCreationModel
    {
        [StringLength(128)]
        public string VacationID { get; set; }

        [StringLength(128)]
        public string EmployeeID { get; set; }

        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBegin { get; set; }

        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEnd { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        
        [StringLength(128)]
        public string VacationTypeID { get; set; }

        
        [StringLength(128)]
        public string VacationStatusTypeID { get; set; }

        public int Duration { get; set; }

        [Column(TypeName = "date")]
        public DateTime Created { get; set; }
    }
}