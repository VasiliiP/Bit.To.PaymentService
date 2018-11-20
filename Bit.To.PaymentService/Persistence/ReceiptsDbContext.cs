using Bit.Persistence;
using Bit.To.PaymentService.Models;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Bit.To.PaymentService.Persistence
{
    public class ReceiptsDbContext
    {
        private const string Table = "Receipts";

        [Table(Table)]
        public class Dto
        {
            public long Id { get; set; }
            public Guid UID { get; set; }
            public int StatusCode { get; set; }
            public string StatusName { get; set; }
            public string StatusMessage { get; set; }
            public DateTime Modified { get; set; }
            public DateTime ReceiptDate { get; set; }
            public string InvoiceId { get; set; }
            public string Type { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Iplace { get; set; }
            public string Iaddress { get; set; }
            public string Dnumber { get; set; }
            public string Inn { get; set; }
            public int DeviceId { get; set; }
            public string Rnm { get; set; }
            public string Zn { get; set; }
            public string Fn { get; set; }
            public string Fdn { get; set; }
            public string Fpd { get; set; }
            public string TaxSystem { get; set; }
            public List<ItemDto> Items { get; set; }
        }

        [Table("ReceiptItems")]
        public class ItemDto
        {
            public long Id { get; set; }
            public int ReceiptId { get; set; }
            public string Label { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public string Vat { get; set; }
        }

        public static QueryObject<Dto> SelectById(Guid key)
        {
            return new QueryObject<Dto>($"SELECT * FROM {Table} WHERE Id=@key", new { key });
        }

        public class Mapper
        {
            public Dto Convert(ReceiptItem item)
            {
                return new Dto
                {
                    UID = item.ReceiptId,
                    StatusCode = item.StatusCode,
                    StatusName = item.StatusName,
                    StatusMessage = item.StatusMessage,
                    Modified = item.ModifiedDateUtc,
                    ReceiptDate = item.ReceiptDateUtc,
                    InvoiceId = item.InvoiceId,
                    Type = item.Type,
                    Email = item.Email,
                    Phone = item.Phone,
                    Iplace = item.InstallmentPlace,
                    Iaddress = item.InstallmentAddress,
                    Inn = item.Inn,
                    Dnumber = item.AutomaticDeviceNumber,
                    DeviceId = item.CashboxInfoHolder.DeviceId,
                    Rnm = item.CashboxInfoHolder.RNM,
                    Zn = item.CashboxInfoHolder.ZN,
                    Fn = item.CashboxInfoHolder.FN,
                    Fdn = item.CashboxInfoHolder.FDN,
                    Fpd = item.CashboxInfoHolder.FPD,
                    Items = Convert(item.Items),
                    Id = item.Id,
                    TaxSystem = item.TaxationSystem
                };
            }

            private static List<ItemDto> Convert(List<Item> items)
            {
                var list = new List<ItemDto>();
                foreach (var item in items)
                {
                    list.Add(new ItemDto
                    {
                        Label = item.Label,
                        Price = item.Price,
                        Amount = item.Amount,
                        Quantity = item.Quantity
                    });
                }

                return list;
            }

            public ReceiptItem Convert(Dto dto)
            {
                var result = new ReceiptItem
                {
                    ReceiptId = dto.UID,
                    AutomaticDeviceNumber = dto.Dnumber,
                    CashboxInfoHolder = new Cashbox()
                    {
                        DeviceId = dto.DeviceId,
                        FDN = dto.Fdn,
                        FN = dto.Fn,
                        FPD = dto.Fpd,
                        Id = dto.Id,
                        RNM = dto.Rnm,
                        ZN = dto.Zn
                    },
                    Email = dto.Email,
                    Id = dto.Id,
                    Inn = dto.Inn,
                    InstallmentAddress = dto.Iaddress,
                    InstallmentPlace = dto.Iplace,
                    InvoiceId = dto.InvoiceId,
                    Items = Convert(dto.Items),
                    StatusCode = dto.StatusCode,
                    ReceiptDateUtc = dto.ReceiptDate,
                    ModifiedDateUtc = dto.Modified,
                    StatusName = dto.StatusName,
                    Phone = dto.Phone,
                    Type = dto.Type,
                    StatusMessage = dto.StatusMessage,
                    TaxationSystem = dto.TaxSystem
                };
                return result;
            }

            private static List<Item> Convert(List<ItemDto> dtos)
            {
                var result = new List<Item>();
                foreach (var dto in dtos)
                {
                    result.Add(new Item()
                    {
                        Amount = dto.Amount,
                        CustomerReceiptId = dto.ReceiptId,
                        Id = dto.Id,
                        Label = dto.Label,
                        Price = dto.Price,
                        Quantity = dto.Quantity,
                        Vat = dto.Vat
                    });
                }
                return result;
            }
        }
    }
}


