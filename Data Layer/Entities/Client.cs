namespace Data_Layer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENTS")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            BANK_ACCOUNTS = new HashSet<Bank_Account>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(150)]
        public string CLIENT_NAME { get; set; }

        [Required]
        [StringLength(15)]
        public string IDENTITY_CARD_NUMBER { get; set; }

        [Required]
        [StringLength(10)]
        public string PHONE { get; set; }

        [Required]
        [StringLength(10)]
        public string PERSONAL_NUMBER { get; set; }

        public DateTime UPDATED_TIMESTAMP { get; set; }

        public long BANK_ID { get; set; }

        public virtual Bank BANK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_Account> BANK_ACCOUNTS { get; set; }
    }
}
