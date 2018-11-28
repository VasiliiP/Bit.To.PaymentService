using Bit.To.PaymentService.Models;
using System;
using Bit.To.PaymentService.Abstractions.Commands;

namespace Bit.To.PaymentService.Persistence
{
    public interface IReseiptItemRepository
    {
        Receipt Find(long id);
        Receipt FindByKey(Guid key);
        void Save(Receipt item);
        void Save(CreateReceipt item, Guid guid);
    }
}
