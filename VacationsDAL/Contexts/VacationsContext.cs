using System.Data.Entity;
using VacationsDAL.Entities;

namespace VacationsDAL.Contexts
{
    public partial class VacationsContext : DbContext
    {
        public VacationsContext()
            : base("name=VacationsContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<VacationStatusType> VacationStatusTypes { get; set; }
        public virtual DbSet<VacationType> VacationTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasOptional(e => e.Employee)
                .WithRequired(e => e.AspNetUser);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.TeamLeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Vacations)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeesTeam)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeTeam").MapLeftKey("EmployeeID").MapRightKey("TeamID"));

            modelBuilder.Entity<JobTitle>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.JobTitle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VacationStatusType>()
                .HasMany(e => e.Vacations)
                .WithRequired(e => e.VacationStatusType)
                .WillCascadeOnDelete(false);
        }
    }
}
