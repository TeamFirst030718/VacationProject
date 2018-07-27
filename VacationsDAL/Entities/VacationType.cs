namespace VacationsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VacationType")]
    public partial class VacationType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VacationType()
        {
            Vacation = new HashSet<Vacation>();
        }

        public string VacationTypeID { get; set; }

        [Column("VacationTypeName")]
        [Required]
        [StringLength(128)]     
        public string VacationTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacation> Vacation { get; set; }
    }
}
