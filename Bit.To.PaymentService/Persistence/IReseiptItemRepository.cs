using Bit.To.PaymentService.Models;
using System;
using Bit.To.PaymentService.Abstractions.Commands;

namespace Bit.To.PaymentService.Persistence
{
    public interface IReseiptItemRepository
    {
        ReceiptItem Find(long id);
        ReceiptItem FindByKey(Guid key);
        void Save(ReceiptItem item);
        void Save(CreateReceipt item, Guid guid);
    }
}
