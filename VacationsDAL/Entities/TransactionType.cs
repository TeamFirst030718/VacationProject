using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("TransactionType")]
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string TransactionTypeID { get; set; }

        [Column("TransactionTypeName")]
        [Required]
        [StringLength(30)]
        public string TransactionTypeName{ get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
