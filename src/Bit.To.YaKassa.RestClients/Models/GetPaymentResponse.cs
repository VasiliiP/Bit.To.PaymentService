using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.YaKassa.RestClients.Models
{
    public class GetPaymentResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public bool paid { get; set; }
        public Amount amount { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public DateTime expires_at { get; set; }
        public Metadata metadata { get; set; }
        public PaymentMethod payment_method { get; set; }
        public bool test { get; set; }
    }

    public class PaymentMethod
    {
        public string type { get; set; }
        public string id { get; set; }
        public bool saved { get; set; }
        public Card card { get; set; }
        public string title { get; set; }
    }

    public class Card
    {
        public string first6 { get; set; }
        public string last4 { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string card_type { get; set; }
    }
}
