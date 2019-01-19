using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.YaKassa.Abstractions.Commands;
using Dapper.Contrib.Extensions;

namespace Bit.To.YaKassa.Persistence
{
    public class PaymentsDbContext
    {
        private const string PAYMENTS_TABLE = "Payments";

        [Table(PAYMENTS_TABLE)]
        public class PaymentDto
        {
            public long Id { get; set; }
            public Guid UID { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public string ConfType { get; set; }
            public string ConfRedirect { get; set; }
            public string Descr { get; set; }
        }

        public class Mapper
        {
            public PaymentDto PaymentDtoFromCmd(CreatePayment cmd)
            {
                return new PaymentDto
                {
                    Amount = Convert.ToDecimal(cmd.amount.value, CultureInfo.InvariantCulture),
                    ConfRedirect = cmd.confirmation.return_url,
                    Descr = cmd.description,
                    Currency = cmd.amount.currency,
                    ConfType = cmd.confirmation.type
                };
            }
        }
    }
}
