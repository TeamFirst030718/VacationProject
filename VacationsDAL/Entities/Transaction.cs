using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("Transaction")]
    public partial class Transaction
    {
        public Transaction()
        {
            Vacations = new HashSet<Vacation>();
        }

        public string TransactionID { get; set; }

        public int BalanceChange { get; set; }

        [Required]
        [StringLength(50)]
        public string Discription { get; set; }

        [Required]
        [StringLength(128)]
        public string EmployeeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(128)]
        public string TransactionTypeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
