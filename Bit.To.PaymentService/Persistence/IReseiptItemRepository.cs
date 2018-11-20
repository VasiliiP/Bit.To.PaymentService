using Bit.To.PaymentService.Models;
using System;

namespace Bit.To.PaymentService.Persistence
{
    public interface IReseiptItemRepository
    {
        ReceiptItem Find(long id);
        ReceiptItem FindByKey(Guid key);
        void Save(ReceiptItem item);
    }
}
