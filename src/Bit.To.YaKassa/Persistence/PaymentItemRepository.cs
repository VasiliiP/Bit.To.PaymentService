using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.Persistence;
using Bit.To.YaKassa.Abstractions.Commands;
using Dapper.Contrib.Extensions;

namespace Bit.To.YaKassa.Persistence
{
    public class PaymentItemRepository: IPaymentItemRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly PaymentsDbContext.Mapper _mapper;

        public PaymentItemRepository(IConnectionFactory connectionFactory, PaymentsDbContext.Mapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }
        public void Save(CreatePayment cmd, Guid guid)
        {
            using (var connection = _connectionFactory.Create())
            {
                var receiptDto = _mapper.DtoFromCmd(cmd);
                receiptDto.UID = guid;
                var id = connection.Insert(receiptDto);
            }
        }
    }
}
