using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.To.PaymentService.Abstractions.Models
{
    public class ReceiptItemDto
    {
        public string Label { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Vat { get; set; }

        public ReceiptItemDto(string label, decimal price, decimal quantity, decimal amount, string vat)
        {
            Label = label;
            Price = price;
            Quantity = quantity;
            Amount = amount;
            Vat = vat;
        }

    }
}
