namespace Data_Layer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BANK_BRANCHES")]
    public partial class Bank_Branch
    {
        public long ID { get; set; }

        public long BANK_ID { get; set; }

        public long CITY_ID { get; set; }

        [Required]
        [StringLength(60)]
        public string BRANCH_NAME { get; set; }

        [Required]
        [StringLength(70)]
        public string BRANCH_ADDRESS { get; set; }

        [Required]
        public DateTime UPDATED_TIMESTAMP { get; set; }

        [Required]
        [StringLength(10)]
        public string PHONE { get; set; }

        public virtual City CITY { get; set; }

        public virtual Bank BANK { get; set; }
    }
}
