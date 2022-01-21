using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ManagementServices
{
    public class TransactionManager
    {
        public string SENDER_PERSONAL_NUMBER { get; set; }
        public long SENDER_BANK_ID { get; set; }
        public string IBAN_SENDER { get; set; }
        public string IBAN_RECEIVER { get; set; }
        public decimal SUM { get; set; }
    }
}
