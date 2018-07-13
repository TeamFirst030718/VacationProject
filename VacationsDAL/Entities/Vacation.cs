using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("Vacation")]
    public partial class Vacation
    {
        public string VacationID { get; set; }

        [Required]
        [StringLength(128)]
        public string EmployeeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBegin { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfEnd { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        [Required]
        [StringLength(128)]
        public string VacationStatusTypeID { get; set; }

        [StringLength(128)]
        public string TransactionID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Transaction Transaction { get; set; }

        public virtual VacationStatusType VacationStatusType { get; set; }
    }
}
