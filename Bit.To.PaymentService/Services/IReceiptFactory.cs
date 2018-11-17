using Bit.To.PaymentService.Abstractions.Commands;

namespace Bit.To.PaymentService.Services
{
    public interface IReceiptFactory
    {
        CreateReceipt Create(string inn);
    }
}