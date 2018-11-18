using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Models;

namespace Bit.To.PaymentService.Abstractions.Queries
{
    public class GetToken : Query<FermaAuthDto>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
