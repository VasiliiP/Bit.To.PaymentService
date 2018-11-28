using Bit.Persistence;
using Bit.To.PaymentService.Models;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using Bit.To.PaymentService.Abstractions.Commands;
using Nancy.Routing.Constraints;

namespace Bit.To.PaymentService.Persistence
{
    public class ReceiptsDbContext
    {
        private const string RECEIPTS_TABLE = "Receipts";
        private const string CASHBOXES_TABLE = "Cashboxes";
        private const string RECEIPTITEMS_TABLE = "ReceiptItems";

        [Table(RECEIPTS_TABLE)]
        public class ReceiptDto
        {
            public long Id { get; set; }
            public Guid UID { get; set; }
            public int StatusCode { get; set; }
            public string StatusName { get; set; }
            public string StatusMessage { get; set; }
            public DateTime? Modified { get; set; }
            public DateTime? ReceiptDate { get; set; }
            public string InvoiceId { get; set; }
            public string Type { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Iplace { get; set; }
            public string Iaddress { get; set; }
            public string Dnumber { get; set; }
            public string Inn { get; set; }
            public string TaxSystem { get; set; }
            public long CashboxId { get; set; }
        }

        [Table(RECEIPTITEMS_TABLE)]
        public class ItemDto
        {
            public long Id { get; set; }
            public long ReceiptId { get; set; }
            public string Label { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public string Vat { get; set; }
        }

        [Table(CASHBOXES_TABLE)]
        public class CashboxDto
        {
            public long Id { get; set; }
            public int DeviceId { get; set; }
            public string Rnm { get; set; }
            public string Zn { get; set; }
            public string Fn { get; set; }
            public string Fdn { get; set; }
            public string Fpd { get; set; }
        }


        public static QueryObject<ReceiptDto> SelectById(Guid key)
        {
            return new QueryObject<ReceiptDto>($"SELECT * FROM {RECEIPTS_TABLE} WHERE Id=@key", new { key });
        }

        public class Mapper
        {

            #region Dto from item

            public ReceiptDto ReceiptDtoFromJson(Receipt item)
            {
                return new ReceiptDto
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
                    Id = item.Id,
                    TaxSystem = item.TaxationSystem
                };
            }

            public CashboxDto CashboxDtoFromJson(Receipt item)
            {
                return new CashboxDto
                {
                    DeviceId = item.CashboxInfoHolder.DeviceId,
                    Rnm = item.CashboxInfoHolder.RNM,
                    Zn = item.CashboxInfoHolder.ZN,
                    Fn = item.CashboxInfoHolder.FN,
                    Fdn = item.CashboxInfoHolder.FDN,
                    Fpd = item.CashboxInfoHolder.FPD,
                    Id = item.Id,
                };
            }

            public List<ItemDto> ListItemsDtoFromJson(List<ReceiptItem> items, long receiptId)
            {
                var list = new List<ItemDto>();
                foreach (var item in items)
                {
                    list.Add(new ItemDto
                    {
                        ReceiptId = receiptId,
                        Label = item.Label,
                        Price = item.Price,
                        Amount = item.Amount,
                        Quantity = item.Quantity
                    });
                }
                return list;
            }

            public ReceiptDto ReceiptDtoFromJson(CreateReceipt cmd)
            {
                var dto = new ReceiptDto()
                {
                    InvoiceId = cmd.Request.InvoiceId,
                    Type = cmd.Request.Type,
                    Email = cmd.Request.CustomerReceipt.Email,
                    Phone = cmd.Request.CustomerReceipt.Phone,
                    Inn = cmd.Request.Inn,
                    TaxSystem = cmd.Request.CustomerReceipt.TaxationSystem
                };
                return dto;
            }

            public List<ItemDto> ListItemsDtoFromJson(List<RecieptItem> items, long receiptId)
            {
                var list = new List<ItemDto>();
                foreach (var item in items)
                {
                    list.Add(new ItemDto
                    {
                        ReceiptId = receiptId,
                        Label = item.Label,
                        Price = item.Price,
                        Amount = item.Amount,
                        Quantity = item.Quantity,
                        Vat = item.Vat
                    });
                }
                return list;
            }

            #endregion

            public Receipt Convert(ReceiptDto dto, CashboxDto cbDto, List<ItemDto> itemsDto)
            {
                var cashbox = Convert(cbDto);
                var items = Convert(itemsDto, dto.UID);

                var result = Receipt.CreateNew(dto.UID, dto.StatusCode, dto.StatusName, dto.StatusMessage, dto.Modified,
                    dto.ReceiptDate, dto.InvoiceId, cashbox, dto.Inn, dto.Type, dto.TaxSystem, dto.Email, dto.Phone,
                    dto.Iplace, dto.Iaddress, dto.Dnumber, items);
               
                return result;
            }

            private static Cashbox Convert(CashboxDto dto)
            {
                return Cashbox.CreateNew(dto.DeviceId, dto.Rnm, dto.Zn, dto.Fn, dto.Fdn, dto.Fpd);
            }

            private static List<ReceiptItem> Convert(List<ItemDto> dtos, Guid receiptId)
            {
                var result = new List<ReceiptItem>();
                foreach (var dto in dtos)
                {
                    result.Add(ReceiptItem.CreateNew(receiptId, dto.Label, dto.Price, dto.Quantity, dto.Amount, dto.Vat));
                }
                return result;
            }
        }
    }
}


