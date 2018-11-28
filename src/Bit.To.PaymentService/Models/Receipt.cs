using System;
using System.Collections.Generic;
using Bit.Domain;

namespace Bit.To.PaymentService.Models
{
    public class Receipt : Entity<long>
    {
        public Guid ReceiptId { get; set; }
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
        public DateTime? ModifiedDateUtc { get; set; }
        public DateTime? ReceiptDateUtc { get; set; }
        public string InvoiceId { get; set; }
        public Cashbox CashboxInfoHolder { get; set; }
        public string Inn { get; set; }
        public string Type { get; set; }
        public string TaxationSystem { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string InstallmentPlace { get; set; }
        public string InstallmentAddress { get; set; }
        public string AutomaticDeviceNumber { get; set; }
        public List<ReceiptItem> Items { get; set; }

        public static Receipt CreateNew(Guid receiptId, int statusCode, string statusName, string statusMessage, DateTime? modifiedDateUtc, DateTime? receiptDateUtc, string invoiceId, Cashbox cashboxInfoHolder, string inn, string type, string taxationSystem, string email, string phone, string installmentPlace, string installmentAddress, string automaticDeviceNumber, List<ReceiptItem> items)
        {
            return new Receipt(receiptId, statusCode, statusName, statusMessage, modifiedDateUtc, receiptDateUtc, invoiceId, cashboxInfoHolder, inn, type, taxationSystem, email, phone, installmentPlace, installmentAddress, automaticDeviceNumber, items);
        }

        private Receipt(Guid receiptId, int statusCode, string statusName, string statusMessage, DateTime? modifiedDateUtc, DateTime? receiptDateUtc, string invoiceId, Cashbox cashboxInfoHolder, string inn, string type, string taxationSystem, string email, string phone, string installmentPlace, string installmentAddress, string automaticDeviceNumber, List<ReceiptItem> items)
        {
            ReceiptId = receiptId;
            StatusCode = statusCode;
            StatusName = statusName;
            StatusMessage = statusMessage;
            ModifiedDateUtc = modifiedDateUtc;
            ReceiptDateUtc = receiptDateUtc;
            InvoiceId = invoiceId;
            CashboxInfoHolder = cashboxInfoHolder;
            Inn = inn;
            Type = type;
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
