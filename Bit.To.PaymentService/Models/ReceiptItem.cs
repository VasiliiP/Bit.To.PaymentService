using System;
using System.Collections.Generic;
using Bit.Domain;

namespace Bit.To.PaymentService.Models
{
    public class ReceiptItem : Entity<long>
    {
            public Guid ReceiptId { get; set; }
            public int StatusCode { get; set; }
            public string StatusName { get; set; }
            public string StatusMessage { get; set; }
            public DateTime ModifiedDateUtc { get; set; }
            public DateTime ReceiptDateUtc { get; set; }
            public string InvoiceId { get; set; }
            public Cashbox CashboxInfoHolder { get; set; }
            public string Inn { get; set; }
            public string Type { get; set; }
            public CustomerReceipt CustomerReceipt { get; set; }
    }


    public class Cashbox : Entity<long>
    {
        public int DeviceId { get; set; }
        public string RNM { get; set; }
        public string ZN { get; set; }
        public string FN { get; set; }
        public string FDN { get; set; }
        public string FPD { get; set; }
    }

    public class CustomerReceipt : Entity<long>
    {
        public string TaxationSystem { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public object InstallmentPlace { get; set; }
        public object InstallmentAddress { get; set; }
        public object AutomaticDeviceNumber { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item : Entity<long>
    {
        public long CustomerReceiptId { get; set; }
        public string Label { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public string Vat { get; set; }
    }

}
