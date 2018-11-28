using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions
{
    public class FermaAuthDto
    {
        public string AuthToken { get; set; }
        public DateTime ExpirationDateUtc { get; set; }

        public FermaAuthDto(string token, DateTime expDate)
        {
            AuthToken = token;
            ExpirationDateUtc = expDate;
        }
    }
}
