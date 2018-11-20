using Bit.To.PaymentService.Abstractions.Commands;

namespace Bit.To.PaymentService.Services
{
    public interface IFermaService
    {
        void TestMethods();
        void GetList();
        ICommandHandler<CreateReceipt> GetCreateRecieptHandler();
    }
}