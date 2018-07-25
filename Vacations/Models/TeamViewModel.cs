using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class TeamViewModel
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamLeadID { get; set; }
    }
}