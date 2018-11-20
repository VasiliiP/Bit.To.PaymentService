using Bit.To.PaymentService.Abstractions;
using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.RestClients.FermaClients;
using Bit.To.PaymentService.RestClients.Logging;
using RestSharp;

namespace Bit.To.PaymentService.RestClients
{
    public class CreateRecieptRestClient : BaseFermaRestClient
    {
        public CreateRecieptRestClient(string token, string baseUrl, string resource):base(token, baseUrl, resource)
        {
        }

        public IRestResponse<CreateReceiptResponse> Execute(CreateReceipt cmd)
        {
            return ExecutePost<CreateReceiptResponse, CreateReceipt>(cmd);
        }
    }
}
