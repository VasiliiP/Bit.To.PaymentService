using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Models;

namespace Bit.To.PaymentService.Services
{
    public interface IReceiptFactory
    {
        CreateReceipt Create(string inn);
        ReceiptItem ItemFromJson(ReceiptItemJson json);
    }
}