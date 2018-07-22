using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationsBLL.DTOs
{
    public class TeamDTO
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamLeadID { get; set; }
    }
}
