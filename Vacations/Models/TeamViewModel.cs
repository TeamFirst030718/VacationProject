using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Vacations.Models
{
    public class TeamViewModel
    {
        public string TeamID { get; set; }

        [Required(ErrorMessage =" required field")]
        [Display(Name = "team name")]
        [Remote("ValidateTeamName", "RemoteValidation", AdditionalFields = "TeamID")]
        [StringLength(50, ErrorMessage = " should be shorter.")]
        public string TeamName { get; set; }

        public string TeamLeadID { get; set; }
    }
}