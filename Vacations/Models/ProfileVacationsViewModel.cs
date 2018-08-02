using System;
using System.ComponentModel.DataAnnotations;

namespace Vacations.Models
{
    public class ProfileVacationsViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBegin { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEnd { get; set; }

        public string Comment { get; set; }

        public string VacationType { get; set; }

        public string Status { get; set; }

        public int Duration { get; set; }
    }
}