using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class Bank_AccountDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank_AccountDTO()
        {
            ACCOUNT_HOLDERS = new HashSet<ClientDTO>();
            IS_ACTIVE = true;
        }

        public long ID { get; set; }

        public long BANK_ID { get; set; }

        public string IBAN { get; set; }

        public bool IS_ACTIVE { get; set; }

        public DateTime UPDATED_TIMESTAMP { get; set; }

        public virtual BankDTO BANK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientDTO> ACCOUNT_HOLDERS { get; set; }
    }
}
