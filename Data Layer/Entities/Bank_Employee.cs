namespace Data_Layer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BANK_EMPLOYEES")]
    public partial class Bank_Employee
    {
        public long ID { get; set; }

        [Required]
        [StringLength(150)]
        public string EMPLOYEE_NAME { get; set; }

        [Column(TypeName = "date")]
        public DateTime BIRTH_DATE { get; set; }

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

        [StringLength(10)]
        public string EXEC_CODE { get; set; }
    }
}
