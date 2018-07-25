using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationsDAL.Entities
{
    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Teams = new HashSet<Team>();
            Transactions = new HashSet<Transaction>();
            Vacations = new HashSet<Vacation>();
            EmployeesTeam = new HashSet<Team>();
        }

        public string EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(256)]
        public string PersonalMail { get; set; }

        [Required]
        [StringLength(60)]
        public string Skype { get; set; }

        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        public bool Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfDismissal { get; set; }

        public int VacationBalance { get; set; }

        [Required]  
        [StringLength(128)]
        public string JobTitleID { get; set; }

        [Required]
        [StringLength(200)]
        public string WorkEmail { get; set; }

        [Required]
        [StringLength(200)]
        public string PhoneNumber { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> Teams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacation> Vacations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team> EmployeesTeam { get; set; }
    }
}
