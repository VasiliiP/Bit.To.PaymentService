using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.YaKassa.RestClients.Models
{
    public class CreatePaymentResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public bool Paid { get; set; }
        public Amount Amount { get; set; }
        public Confirmation Confirmation { get; set; }
        public DateTime Created_at { get; set; }
        public string Description { get; set; }
        public Metadata Metadata { get; set; }
        public bool Test { get; set; }
    }

    public class Amount
    {
        public string Value { get; set; }
        public string Currency { get; set; }
    }

    public class Confirmation
    {
        public string Type { get; set; }
        public string Confirmation_url { get; set; }
    }

    public class Metadata
    {
    }

}
