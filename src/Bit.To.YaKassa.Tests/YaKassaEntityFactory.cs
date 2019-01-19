using Bit.To.YaKassa.Abstractions.Commands;

namespace Bit.To.YaKassa.Tests
{
    public static class YaKassaEntityFactory
    {
        public static CreatePayment CreatePayment()
        {
            return new CreatePayment
            {
                amount = new Amount
                {
                    value = "33.00",
                    currency = "RUB"
                },
                capture = true,
                confirmation = new Confirmation
                {
                    return_url = "https://www.merchant-website.com/return_url",
                    type = "redirect"
                },
                description = "Payment from unit test"
            };
        }
    }
}
