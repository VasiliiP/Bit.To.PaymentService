using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions.Commands
{
    public class CreateReceipt: Command
    {
        public Guid Id { get; set; }
        public ReceiptRequest Request { get; set; }
    }

    public class ReceiptRequest
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
        public ICollection<RecieptItem> Items { get; set; }
    }

    public class RecieptItem
    {
        public string Label { get; set; }
        public decimal Price { get; set; }
        public float Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Vat { get; set; }
    }

    public class ReceiptResponse
    {
        public string Status { get; set; }
        public List<string> Errors { get; set; }

    }

    public class RecieptResponseData
    {
        public string ReceiptId { get; set; }
    }
}
