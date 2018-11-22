using Bit.Persistence;
using Bit.Persistence.Dapper;
using Bit.To.PaymentService.Models;
using Dapper.Contrib.Extensions;
using System;
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

        public ReceiptItem Find(long id)
        {
            using (var connection = _connectionFactory.Create())
            {
                var dto = connection.Get<ReceiptsDbContext.ReceiptDto>(id);
                return dto == null ? null : _mapper.Convert(dto);
            }
        }

        public ReceiptItem FindByKey(Guid key)
        {
            using (var connection = _connectionFactory.Create())
            {
                var dto = connection.QueryFirstOrDefault(ReceiptsDbContext.SelectById(key));
                return dto == null ? null : _mapper.Convert(dto);
            }
        }

        public void Save(ReceiptItem item)
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
