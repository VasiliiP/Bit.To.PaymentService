using Bit.Persistence;
using Bit.Persistence.Dapper;
using Bit.To.PaymentService.Models;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using Bit.To.PaymentService.Abstractions.Commands;

namespace Bit.To.PaymentService.Persistence
{

    public class ReseiptItemRepository : IReseiptItemRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ReceiptsDbContext.Mapper _mapper;

        public ReseiptItemRepository(IConnectionFactory connectionFactory, ReceiptsDbContext.Mapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public Receipt Find(long id)
        {
            using (var connection = _connectionFactory.Create())
            {
                var receiptDto = connection.Get<ReceiptsDbContext.ReceiptDto>(id);
                var cashBoxDto = connection.Get<ReceiptsDbContext.CashboxDto>(receiptDto.CashboxId);
                var itemsDto = new List<ReceiptsDbContext.ItemDto>();//TODO: запрос на получение коллекции
                return receiptDto == null ? null : _mapper.Convert(receiptDto, cashBoxDto, itemsDto);
            }
        }

        public Receipt FindByKey(Guid key)
        {
            using (var connection = _connectionFactory.Create())
            {
                var receiptDto = connection.QueryFirstOrDefault(ReceiptsDbContext.SelectById(key));
                var cashBoxDto = connection.Get<ReceiptsDbContext.CashboxDto>(receiptDto.CashboxId);
                var itemsDto = new List<ReceiptsDbContext.ItemDto>();//TODO: запрос на получение коллекции
                return receiptDto == null ? null : _mapper.Convert(receiptDto, cashBoxDto, itemsDto);
            }
        }

        public void Save(Receipt item)
        {
            using (var connection = _connectionFactory.Create())
            {
                var dto = _mapper.ReceiptDtoFromJson(item);
                if (!item.HasId)
                {
                    connection.Insert(dto);
                    item.Id = dto.Id;
                }
                else
                    connection.Update(dto);
            }
        }

        public void Save(CreateReceipt cmd, Guid guid)
        {
            using (var connection = _connectionFactory.Create())
            {
                var receiptDto = _mapper.ReceiptDtoFromJson(cmd);
                receiptDto.UID = guid;
                var id = connection.Insert(receiptDto);

                var listItemsDto = _mapper.ListItemsDtoFromJson(cmd.Request.CustomerReceipt.Items, id);
                connection.Insert(listItemsDto);
            }
        }
    }
}
