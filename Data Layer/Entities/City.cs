namespace Data_Layer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITIES")]
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            BANK_BRANCHES = new HashSet<Bank_Branch>();
            BANKS = new HashSet<Bank>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(35)]
        public string CITY_NAME { get; set; }
        [Required]
        public DateTime UPDATED_TIMESTAMP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_Branch> BANK_BRANCHES { get; set; }
        public virtual ICollection<Bank> BANKS { get; set; }
    }
}
