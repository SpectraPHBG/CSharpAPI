namespace Data_Layer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BANKS")]
    public partial class Bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank()
        {
            BANK_ACCOUNTS = new HashSet<Bank_Account>();
            BANK_EMPLOYEES = new HashSet<Bank_Employee>();
            BANK_BRANCHES = new HashSet<Bank_Branch>();
            CLIENTS = new HashSet<Client>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(90)]
        public string BANK_NAME { get; set; }

        [Required]
        [StringLength(8)]
        public string BIC { get; set; }

        public long CITY_CENTRAL_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string PHONE { get; set; }

        public virtual City CITY { get; set; }

        [Required]
        public DateTime UPDATED_TIMESTAMP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_Account> BANK_ACCOUNTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_Branch> BANK_BRANCHES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> CLIENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_Employee> BANK_EMPLOYEES { get; set; }
    }
}
