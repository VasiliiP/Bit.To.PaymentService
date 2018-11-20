using Bit.To.PaymentService.Abstractions.Commands;

namespace Bit.To.PaymentService.Services
{
    public interface IFermaService
    {
        string GetToken();
        void GetList();
    }
}