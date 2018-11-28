using System;
using System.Collections.Generic;

namespace Bit.To.PaymentService.RestClients
{
    public class ReceiptsListResponse: BaseFermaResponse
    { 
        public List<ReceiptJson> Data { get; set; }
    }

    public class ReceiptJson
    {
        public string ReceiptId { get; set; }
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public DateTime ReceiptDateUtc { get; set; }
        public string InvoiceId { get; set; }
        public ReceiptResponseJson Receipt { get; set; }
    }

    public class ReceiptResponseJson
    {
        public CashboxJson cashboxInfoHolder { get; set; }
        public string Inn { get; set; }
        public string Type { get; set; }
        public string InvoiceId { get; set; }
        public CustomerReceiptJson CustomerReceipt { get; set; }
    }

    public class CashboxJson
    {
        public int DeviceId { get; set; }
        public string RNM { get; set; }
        public string ZN { get; set; }
        public string FN { get; set; }
        public string FDN { get; set; }
        public string FPD { get; set; }
    }

    public class CustomerReceiptJson
    {
        public string TaxationSystem { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string InstallmentPlace { get; set; }
        public string InstallmentAddress { get; set; }
        public string AutomaticDeviceNumber { get; set; }
        public List<ItemJson> Items { get; set; }
    }

    public class ItemJson
    {
        public string Label { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Vat { get; set; }
    }
}
