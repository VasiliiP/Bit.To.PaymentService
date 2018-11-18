using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Abstractions.Models;

namespace Bit.To.PaymentService.Abstractions.Queries
{

    public class GetReceiptsList: Query<ReceiptsListDto>
    {
        public GetReceiptsListRequest Request { get; set; }
    }

    public class GetReceiptsListRequest
    {
        public string ReceiptId { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public DateTime? StartDateLocal { get; set; }
        public DateTime? EndDateLocal { get; set; }
    }


}
