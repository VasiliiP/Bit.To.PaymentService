using Bit.To.PaymentService.Abstractions.Models;
using System;

namespace Bit.To.PaymentService.Abstractions.Queries
{
    public class GetReceiptStatus : Query<ReceiptStatusDto>
    {
        public Request Request { get; set; }
    }
    public class Request
    {
        public string ReceiptId { get; set; }
    }

}
