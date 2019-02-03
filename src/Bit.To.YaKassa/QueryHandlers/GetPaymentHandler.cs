using Bit.To.YaKassa.Abstractions;

namespace Bit.To.YaKassa.QueryHandlers
{
    public class GetPaymentHandler : IQueryHandler<GetPayment, PaymentDto>
    {
        private readonly string _endpoint;
        private readonly string _shopId;
        private readonly string _secret;


        public PaymentDto Execute(GetPayment query)
        {

            var payment = new PaymentDto();
            return payment;
        }
    }
}
