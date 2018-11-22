using System;
using System.Collections.Generic;

namespace Bit.To.PaymentService.Abstractions.Commands
{
    public class CreateReceipt : Command
    {
        public CreateReceiptRequest Request { get; set; }
    }

    public class CreateReceiptRequest
    {
        public string Inn { get; set; }
        public string Type { get; set; }
        public string InvoiceId { get; set; }
        public DateTime LocalDate { get; set; }
        public CustomerReceipt CustomerReceipt { get; set; }
    }

    public class CustomerReceipt
    {
        public string TaxationSystem { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<RecieptItem> Items { get; set; }
    }

    public class RecieptItem
    {
        public string Label { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Vat { get; set; }
    }

}


