using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class Bank_AccountDetailedDTO : Bank_AccountDTO
    {
        public Bank_AccountDetailedDTO():base()
        {
            CURRENT_BALANCE = -1;
        }
        public decimal CURRENT_BALANCE { get; set; }

        public DateTime CREATED_AT { get; set; }

    }
}
