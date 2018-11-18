using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.Logging;
using System;
using System.Net;
using System.Text;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetReceiptStatusRestClient : BaseFermaRestClient, IQueryHandler<GetReceiptStatus, ReceiptStatusDto>
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public GetReceiptStatusRestClient(string token, string baseUrl, string resource):base(token, baseUrl, resource)
        {
        }

        public ReceiptStatusDto Execute(GetReceiptStatus query)
        {
            var response = ExecutePost<ReceiptStatusDto, GetReceiptStatus>(query);

            if (response == null)
                return null;

            var payload = response.Data;
            Log.DebugFormat("{0} Ferma response is: Status:{1}, Payload:{2}", 
                response.Request.Resource, payload.Status, payload.Data.StatusMessage);

            return payload;
        }
    }
}
