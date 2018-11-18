using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions.Models
{
    public class BaseFermaResponse
    {
        public string Status { get; set; }
        public BaseFermaResponseError Error { get; set; }
    }

    public class BaseFermaResponseError
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
