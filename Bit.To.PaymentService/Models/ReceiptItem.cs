using System;
using Bit.Domain;

namespace Bit.To.PaymentService.Models
{
    public class ReceiptItem : Entity<long>
    {
        public Guid ReceiptId { get; }
        public string Label { get; }
        public decimal Price { get; }
        public decimal Quantity { get; }
        public decimal Amount { get; }
        public string Vat { get; }

        private ReceiptItem(Guid receiptId, string label, decimal price, decimal quantity, decimal amount, string vat)
        {
            ReceiptId = receiptId;
            Label = label;
            Price = price;
            Quantity = quantity;
            Amount = amount;
            Vat = vat;
        }

        public static ReceiptItem CreateNew(Guid receiptId, string label, decimal price, decimal quantity, decimal amount, string vat)
        {
            return new ReceiptItem(receiptId, label,  price,  quantity,  amount,  vat);
        }
    }
}