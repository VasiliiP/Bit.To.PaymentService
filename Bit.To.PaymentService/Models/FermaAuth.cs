using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Models
{
    public class FermaAuth
    {
        public FermaAuthData Data { get; set; }
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Data.AuthToken))
                    return false;

                return Data.ExpirationDateUtc > DateTime.UtcNow;
            }
        }
    }

    public class FermaAuthData
    {
        public string AuthToken { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }

}
