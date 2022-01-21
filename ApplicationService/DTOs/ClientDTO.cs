using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
     public class ClientDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientDTO()
        {
            BANK_ACCOUNTS = new HashSet<Bank_AccountDTO>();
        }

        public long ID { get; set; }

        public long BANK_ID { get; set; }

        public string CLIENT_NAME { get; set; }

        public DateTime UPDATED_TIMESTAMP { get; set; }

        public virtual BankDTO BANK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_AccountDTO> BANK_ACCOUNTS { get; set; }
    }
}
