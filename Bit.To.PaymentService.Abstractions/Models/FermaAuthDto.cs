using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Abstractions.Models;

namespace Bit.To.PaymentService.Models
{
    public class FermaAuthDto: BaseFermaResponse
    {
        public FermaAuthData Data { get; set; }
    }

    public class FermaAuthData
    {
        public string AuthToken { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }

}
