using Bit.To.PaymentService.Abstractions.Models;
using System;

namespace Bit.To.PaymentService.Abstractions.Queries
{
    public class GetReceiptStatus : Query<ReceiptStatusResponse>
    {
        public Request Request { get; set; }
    }
    public class Request
    {
        public string ReceiptId { get; set; }
    }

}
