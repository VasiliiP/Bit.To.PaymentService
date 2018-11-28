using Bit.To.PaymentService.Abstractions.Models;
using System;

namespace Bit.To.PaymentService.Abstractions.Queries
{
    public class GetReceiptStatus : Query<ReceiptStatusDto>
    {
        public GetReceiptRequest Request { get; set; }
    }
    public class GetReceiptRequest
    {
        public string ReceiptId { get; set; }
    }

}
