using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions.Models
{
    public class ReceiptsListResponse: BaseFermaResponse
    { 
        public List<Datum> Data { get; set; }
    }

    public class Datum
    {
        public string ReceiptId { get; set; }
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public DateTime ReceiptDateUtc { get; set; }
        public string InvoiceId { get; set; }
        public Receipt Receipt { get; set; }
    }

    public class Receipt
    {
        public Cashboxinfoholder cashboxInfoHolder { get; set; }
        public string Inn { get; set; }
        public string Type { get; set; }
        public string InvoiceId { get; set; }
        public Customerreceipt CustomerReceipt { get; set; }
    }

    public class Cashboxinfoholder
    {
        public int DeviceId { get; set; }
        public string RNM { get; set; }
        public string ZN { get; set; }
        public string FN { get; set; }
        public string FDN { get; set; }
        public string FPD { get; set; }
    }

    public class Customerreceipt
    {
        public string TaxationSystem { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public object InstallmentPlace { get; set; }
        public object InstallmentAddress { get; set; }
        public object AutomaticDeviceNumber { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Label { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public string Vat { get; set; }
    }
}
