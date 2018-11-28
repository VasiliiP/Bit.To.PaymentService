using System;
using System.Collections.Generic;

namespace Bit.To.PaymentService.RestClients
{
    public class ReceiptStatusResponse: BaseFermaResponse
    {
        public ReceiptStatusData Data { get; set; }
    }

    public class ReceiptStatusData
    {
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public DateTime ReceiptDateUtc { get; set; }
        public Device Device { get; set; }
    }

    public class Device
    {
        public int DeviceId { get; set; }
        public string RNM { get; set; }
        public string ZN { get; set; }
        public string FN { get; set; }
        public string FDN { get; set; }
        public string FPD { get; set; }
    }
}
