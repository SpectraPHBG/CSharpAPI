namespace Data_Layer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BANK_ACCOUNTS")]
    public partial class Bank_Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank_Account()
        {
            ACCOUNT_HOLDERS = new HashSet<Client>();
        }

        public long ID { get; set; }

        public long BANK_ID { get; set; }

        [Required]
        [StringLength(22)]
        public string IBAN { get; set; }

        public decimal CURRENT_BALANCE { get; set; }

        [Required]
        public DateTime CREATED_AT { get; set; }

        public bool IS_ACTIVE { get; set; }

        [Required]
        public DateTime UPDATED_TIMESTAMP { get; set; }

        public virtual Bank BANK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> ACCOUNT_HOLDERS { get; set; }
    }
}
