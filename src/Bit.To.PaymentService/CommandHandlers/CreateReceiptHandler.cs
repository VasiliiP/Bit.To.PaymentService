using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Persistence;
using Bit.To.PaymentService.RestClients;
using System;
using Bit.To.PaymentService.Abstractions.Queries;

namespace Bit.To.PaymentService.CommandHandlers
{
    public class CreateReceiptHandler : ICommandHandler<CreateReceipt>
    {
        private readonly IReseiptItemRepository _repository;
        private readonly string _endpoint;
        private readonly IQueryHandler<GetToken, string> _tokenHandler;

        public CreateReceiptHandler(string endpoint, IReseiptItemRepository repository, IQueryHandler<GetToken, string> tokenHandler)
        {
            _endpoint = endpoint;
            _repository = repository;
            _tokenHandler = tokenHandler;
        }

        public void Execute(CreateReceipt cmd)
        {
            var restClient = new CreateRecieptRestClient(_endpoint, _tokenHandler);
            var receiptId = restClient.Execute(cmd);
            _repository.Save(cmd, new Guid(receiptId));
        }
    }
}
