using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Persistence;
using Bit.To.PaymentService.RestClients;
using Bit.To.PaymentService.Services;
using System;

namespace Bit.To.PaymentService.CommandHandlers
{
    public class CreateReceiptHandler : ICommandHandler<CreateReceipt>
    {
        private readonly IFermaService _fermaService;
        private readonly IReseiptItemRepository _repository;
        private readonly string _endpoint;

        public CreateReceiptHandler(IFermaService fermaService, string endpoint, IReseiptItemRepository repository)
        {
            _fermaService = fermaService;
            _endpoint = endpoint;
            _repository = repository;
        }

        public void Execute(CreateReceipt cmd)
        {
            var restClient = new CreateRecieptRestClient(_endpoint, _fermaService.GetToken());
            var receiptId = restClient.Execute(cmd);
            _repository.Save(cmd, new Guid(receiptId));
        }
    }
}
