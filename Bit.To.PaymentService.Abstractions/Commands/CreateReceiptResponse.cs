using Bit.To.PaymentService.Abstractions.Models;

namespace Bit.To.PaymentService.Abstractions.Commands
{
    public class CreateReceiptResponse: BaseFermaResponse
    {
        public CreateReceiptResponseData Data { get; set; }
    }
    public class CreateReceiptResponseData
    {
        public string ReceiptId { get; set; }
    }
}