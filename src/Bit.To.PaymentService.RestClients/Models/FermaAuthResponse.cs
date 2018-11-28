using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Abstractions.Models;

namespace Bit.To.PaymentService.RestClients
{
    public class FermaAuthResponse: BaseFermaResponse
    {
        public FermaAuthData Data { get; set; }
    }

    public class FermaAuthData
    {
        public string AuthToken { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }

}
