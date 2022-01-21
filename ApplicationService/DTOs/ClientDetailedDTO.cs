using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class ClientDetailedDTO : ClientDTO
    {
        public string IDENTITY_CARD_NUMBER { get; set; }

        public string PHONE { get; set; }

        public string PERSONAL_NUMBER { get; set; }
    }
}
