using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPAYMENT.Models
{
    public class PaymentSettings
    {
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string StoreKey { get; set; }
        string SuccessUrl { get; set; }
        string ErrorUrl { get; set; }
    }
}
