using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions.Models
{
    public class ReceiptDto
    {
        public string ReceiptId { get; set; }
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public DateTime ReceiptDateUtc { get; set; }
        public string InvoiceId { get; set; }
        public string Inn { get; set; }
        public string Type { get; set; }
        public int DeviceId { get; set; }
        public string RNM { get; set; }
        public string ZN { get; set; }
        public string FN { get; set; }
        public string FDN { get; set; }
        public string FPD { get; set; }
        public string TaxationSystem { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string InstallmentPlace { get; set; }
        public string InstallmentAddress { get; set; }
        public string AutomaticDeviceNumber { get; set; }
        public List<ReceiptItemDto> Items { get; set; }

        public ReceiptDto(string receiptId, int statusCode, string statusName, string statusMessage, DateTime modifiedDateUtc, DateTime receiptDateUtc, string invoiceId, string inn, string type, int deviceId, string rNM, string zN, string fN, string fDN, string fPD, string taxationSystem, string email, string phone, string installmentPlace, string installmentAddress, string automaticDeviceNumber, List<ReceiptItemDto> items)
        {
            ReceiptId = receiptId;
            StatusCode = statusCode;
            StatusName = statusName;
            StatusMessage = statusMessage;
            ModifiedDateUtc = modifiedDateUtc;
            ReceiptDateUtc = receiptDateUtc;
            InvoiceId = invoiceId;
            Inn = inn;
            Type = type;
            DeviceId = deviceId;
            RNM = rNM;
            ZN = zN;
            FN = fN;
            FDN = fDN;
            FPD = fPD;
            TaxationSystem = taxationSystem;
            Email = email;
            Phone = phone;
            InstallmentPlace = installmentPlace;
            InstallmentAddress = installmentAddress;
            AutomaticDeviceNumber = automaticDeviceNumber;
            Items = items;
        }

    }
}
