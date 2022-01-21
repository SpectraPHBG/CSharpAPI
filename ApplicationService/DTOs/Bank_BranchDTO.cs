using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class Bank_BranchDTO
    {
        public long ID { get; set; }

        public long BANK_ID { get; set; }

        public long CITY_ID { get; set; }

        public string BRANCH_NAME { get; set; }

        public string BRANCH_ADDRESS { get; set; }

        public string PHONE { get; set; }

        public DateTime UPDATED_TIMESTAMP { get; set; }

        public virtual CityDTO CITY { get; set; }

        public virtual BankDTO BANK { get; set; }
    }
}
