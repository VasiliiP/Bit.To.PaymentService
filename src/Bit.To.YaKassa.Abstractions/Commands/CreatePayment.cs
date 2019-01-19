using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.YaKassa.Abstractions.Commands
{
    public class CreatePayment : Command
    {
        public Amount amount { get; set; }
        public Confirmation confirmation { get; set; }
        public bool capture { get; set; }
        public string description { get; set; }
    }
    public class Amount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class Confirmation
    {
        public string type { get; set; }
        public string return_url { get; set; }
    }
}
