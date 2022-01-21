using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class BankDTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankDTO()
        {
            BANK_ACCOUNTS = new HashSet<Bank_AccountDTO>();
            BANK_BRANCHES = new HashSet<Bank_BranchDTO>();
            CLIENTS = new HashSet<ClientDetailedDTO>();
        }

        public long ID { get; set; }

        public string BANK_NAME { get; set; }

        public string BIC { get; set; }

        public long CITY_CENTRAL_ID { get; set; }

        public string PHONE { get; set; }

        public virtual CityDTO CITY { get; set; }

        public DateTime UPDATED_TIMESTAMP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_AccountDTO> BANK_ACCOUNTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_BranchDTO> BANK_BRANCHES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientDetailedDTO> CLIENTS { get; set; }
    }
}
