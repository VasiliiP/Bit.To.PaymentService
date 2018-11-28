using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions.Models
{
    public class ReceiptStatusDto
    {
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public DateTime ReceiptDateUtc { get; set; }
        public int DeviceId { get; set; }
        public string RNM { get; set; }
        public string ZN { get; set; }
        public string FN { get; set; }
        public string FDN { get; set; }
        public string FPD { get; set; }

        public ReceiptStatusDto(int statusCode, string statusName, string statusMessage, DateTime modifiedDateUtc, DateTime receiptDateUtc, int deviceId, string rnm, string zn, string fn, string fdn, string fpd)
        {
            StatusCode = statusCode;
            StatusName = statusName;
            StatusMessage = statusMessage;
            ModifiedDateUtc = modifiedDateUtc;
            ReceiptDateUtc = receiptDateUtc;
            DeviceId = deviceId;
            RNM = rnm;
            ZN = zn;
            FN = fn;
            FDN = fdn;
            FPD = fpd;
        }
    }
}
