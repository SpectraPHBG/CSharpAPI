using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class CityDTO
    {
        public CityDTO()
        {
            BANK_BRANCHES = new HashSet<Bank_BranchDTO>();
            BANKS = new HashSet<BankDTO>();
        }
        public long ID { get; set; }

        public string CITY_NAME { get; set; }

        public DateTime UPDATED_TIMESTAMP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_BranchDTO> BANK_BRANCHES { get; set; }
        public virtual ICollection<BankDTO> BANKS { get; set; }
    }
}
