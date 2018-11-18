using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Abstractions.Commands;

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
                                Quantity = 12.00f,
                                Price = 40.00M,
                                Amount = 480M,
                                Vat = "Vat10"
                            },
                            new RecieptItem
                            {
                                Label = "Cucumbers",
                                Quantity = 10.00f,
                                Price = 40.00M,
                                Amount = 400M,
                                Vat = "Vat10"
                            }
                        }
                    }
                }
            };
        }
    }
}
