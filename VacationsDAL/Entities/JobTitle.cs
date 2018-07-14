using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("JobTitle")]
    public partial class JobTitle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobTitle()
        {
            Employees = new HashSet<Employee>();
        }

        public string JobTitleID { get; set; }

        [Column("JobTitle")]
        [StringLength(50)]
        public string JobTitle1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
