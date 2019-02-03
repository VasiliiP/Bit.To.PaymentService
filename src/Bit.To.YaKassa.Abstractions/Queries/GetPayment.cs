using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.YaKassa.Abstractions
{
    public class GetPayment : Query<PaymentDto>
    {
        public string PaymentId { get; set; }
    }
}
