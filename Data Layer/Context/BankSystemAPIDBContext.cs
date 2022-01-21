namespace Data_Layer.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BankSystemAPIDBContext : DbContext
    {
        public BankSystemAPIDBContext()
            : base("name=BankSystemAPIDBContext")
        {
        }

        public virtual DbSet<Bank_Account> BANK_ACCOUNTS { get; set; }
        public virtual DbSet<Bank_Branch> BANK_BRANCHES { get; set; }
        public virtual DbSet<Bank> BANKS { get; set; }
        public virtual DbSet<City> CITIES { get; set; }
        public virtual DbSet<Client> CLIENTS { get; set; }
        public virtual DbSet<Bank_Employee> BANK_EMPLOYEES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank_Account>()
                .Property(e => e.IBAN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Bank_Account>()
                .HasMany(e => e.ACCOUNT_HOLDERS)
                .WithMany(e => e.BANK_ACCOUNTS)
                .Map(m => m.ToTable("BANK_ACCOUNT_OWNERS").MapLeftKey("ACCOUNT_ID").MapRightKey("HOLDER_ID"));

            modelBuilder.Entity<Bank_Branch>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .Property(e => e.CITY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.BANK_BRANCHES)
                .WithRequired(e => e.CITY)
                .HasForeignKey(e => e.CITY_ID);

            modelBuilder.Entity<Bank>()
                .Property(e => e.BIC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.BANK_ACCOUNTS)
                .WithRequired(e => e.BANK)
                .HasForeignKey(e => e.BANK_ID);

            modelBuilder.Entity<City>()
            .HasMany(e => e.BANKS)
            .WithRequired(e => e.CITY)
            .HasForeignKey(e => e.CITY_CENTRAL_ID);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.BANK_BRANCHES)
                .WithRequired(e => e.BANK)
                .HasForeignKey(e => e.BANK_ID);

            modelBuilder.Entity<Client>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.CLIENTS)
                .WithRequired(e => e.BANK)
                .HasForeignKey(e => e.BANK_ID);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.BANK_EMPLOYEES)
                .WithRequired(e => e.BANK)
                .HasForeignKey(e => e.BANK_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.PERSONAL_NUMBER)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
