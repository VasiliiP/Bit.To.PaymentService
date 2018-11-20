using System;
using Bit.To.PaymentService.Abstractions;
using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Logging;
using Bit.To.PaymentService.Persistence;
using Bit.To.PaymentService.RestClients;
using Bit.To.PaymentService.Services;

namespace Bit.To.PaymentService.CommandHandlers
{
    public class CreateReceiptHandler : ICommandHandler<CreateReceipt>
    {
        public CreateReceiptResponse Response { get; private set; }
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        private readonly IFermaService _fermaService;
        private readonly IReseiptItemRepository _repository;
        private readonly string _fermaBaseUrl;
        private readonly string _createReceiptResource;

        public CreateReceiptHandler(IFermaService fermaService, string fermaBaseUrl, string createReceiptResource, IReseiptItemRepository repository)
        {
            _fermaService = fermaService;
            _fermaBaseUrl = fermaBaseUrl;
            _createReceiptResource = createReceiptResource;
            _repository = repository;
        }

        public void Execute(CreateReceipt cmd)
        {
            var restClient = new CreateRecieptRestClient(_fermaService.GetToken(), _fermaBaseUrl, _createReceiptResource);
            var response = restClient.Execute(cmd);
            if (response == null || response.ErrorException != null || !string.Equals(response.Data.Status, "Success"))
                throw new Exception();

            var payload = response.Data;
            Log.DebugFormat("{0} Ferma response is: Status:{1}, ReceiptId:{2}",
                response.Request.Resource, payload.Status, payload.Data.ReceiptId);
            Response = payload;
            
            //_repository.Save(cmd, new Guid(payload.Data.ReceiptId));
        }
    }
}
