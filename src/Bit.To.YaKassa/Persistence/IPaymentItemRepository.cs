using System;
using Bit.To.YaKassa.Abstractions.Commands;

namespace Bit.To.YaKassa.Persistence
{
    public interface IPaymentItemRepository
    {
        void Save(CreatePayment item, Guid guid);
    }
}