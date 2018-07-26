using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("VacationStatusType")]
    public partial class VacationStatusType
    {
        public VacationStatusType()
        {
            Vacations = new HashSet<Vacation>();
        }

        public string VacationStatusTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string VacationStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
