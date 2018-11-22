using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Models;
using System;
using System.Collections.Generic;

namespace Bit.To.PaymentService.Services
{
    public class ReceiptFactory: IReceiptFactory
    {
        public CreateReceipt Create(string inn)
        {
            var seed = (uint)DateTime.Now.Ticks;
            return new CreateReceipt
            {
                Request = new CreateReceiptRequest
                {
                    Inn = inn,
                    InvoiceId = "Id1" + seed,
                    LocalDate = DateTime.UtcNow,
                    Type = "Income",
                    CustomerReceipt = new CustomerReceipt
                    {
                        Email = "asd@dsa.ru",
                        Phone = "89991234567",
                        TaxationSystem = "Common",
                        Items = new List<RecieptItem>
                        {
                            new RecieptItem
                            {
                                Label = "Tomates",
                                Quantity = 12.00M,
                                Price = 40.00M,
                                Amount = 480M,
                                Vat = "Vat10"
                            },
                            new RecieptItem
                            {
                                Label = "Cucumbers",
                                Quantity = 10.00M,
                                Price = 40.00M,
                                Amount = 400M,
                                Vat = "Vat10"
                            }
                        }
                    }
                }
            };
        }

        public ReceiptItem ItemFromJson(ReceiptItemJson json)
        {
            var item = new ReceiptItem
            {
                ReceiptId = new Guid(json.ReceiptId),
                StatusCode = json.StatusCode,
                StatusName = json.StatusName,
                StatusMessage = json.StatusMessage,
                ModifiedDateUtc = json.ModifiedDateUtc,
                ReceiptDateUtc = json.ReceiptDateUtc,
                InvoiceId = json.InvoiceId,
                CashboxInfoHolder = new Cashbox
                {
                    DeviceId = json.Receipt.cashboxInfoHolder.DeviceId,
                    FDN = json.Receipt.cashboxInfoHolder.FDN,
                    FN = json.Receipt.cashboxInfoHolder.FN,
                    FPD = json.Receipt.cashboxInfoHolder.FPD,
                    RNM = json.Receipt.cashboxInfoHolder.RNM,
                    ZN = json.Receipt.cashboxInfoHolder.ZN
                },
                Inn = json.Receipt.Inn,
                Type = json.Receipt.Type,
                TaxationSystem = json.Receipt.CustomerReceipt.TaxationSystem,
                Email = json.Receipt.CustomerReceipt.Email,
                Phone = json.Receipt.CustomerReceipt.Phone,
                InstallmentAddress = json.Receipt.CustomerReceipt.InstallmentAddress,
                InstallmentPlace = json.Receipt.CustomerReceipt.InstallmentPlace,
                AutomaticDeviceNumber = json.Receipt.CustomerReceipt.AutomaticDeviceNumber
            };
            return item;
        }

        //private List<Item> ItemsFromJson(List<ItemJson>)
        //{
        //    var result = new List<Item>()
        //}
    }
}
