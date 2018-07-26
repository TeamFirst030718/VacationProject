using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("JobTitle")]
    public partial class JobTitle
    {
        public JobTitle()
        {
            Employees = new HashSet<Employee>();
        }

        public string JobTitleID { get; set; }

        [Column("JobTitle")]
        [StringLength(50)]
        public string JobTitleName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
