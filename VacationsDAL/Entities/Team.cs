using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            Employees = new HashSet<Employee>();
        }

        public string TeamID { get; set; }

        [Required]
        [StringLength(50)]
        public string TeamName { get; set; }

        [Required]
        [StringLength(128)]
        public string TeamLeadID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
