using Bit.To.PaymentService.Abstractions;
using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.RestClients.FermaClients;
using Bit.To.PaymentService.RestClients.Logging;

namespace Bit.To.PaymentService.RestClients
{
    public class CreateRecieptRestClient : BaseFermaRestClient, ICommandHandler<CreateReceipt>
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        //TODO временно, пока не реализована БД
        public CreateReceiptResponse Response { get; set; }

        public CreateRecieptRestClient(string token, string baseUrl, string resource):base(token, baseUrl, resource)
        {
        }

        public void Execute(CreateReceipt cmd)
        {
            var response = ExecutePost<CreateReceiptResponse, CreateReceipt>(cmd);

            if (response == null)
                return;

            var payload = response.Data;
            Log.DebugFormat("{0} Ferma response is: Status:{1}, Payload:{2}",
                response.Request.Resource, payload.Status, payload.Data.ReceiptId);
            Response = payload;
        }

    }
}
