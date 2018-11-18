using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.Logging;
using System.Linq;
using System.Net;
using System.Text;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetReceiptsListRestClient : BaseFermaRestClient, IQueryHandler<GetReceiptsList, ReceiptsListResponse>
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public GetReceiptsListRestClient(string token, string baseUrl, string resource) : base(token, baseUrl, resource)
        {
        }

        public ReceiptsListResponse Execute(GetReceiptsList query)
        {
            var response = ExecutePost<ReceiptsListResponse, GetReceiptsList>(query);

            if (response == null)
                return null;

            var payload = response.Data;
            Log.DebugFormat("{0} Ferma response is: Status:{1}, Receipts Count: {2}", 
                response.Request.Resource, payload.Status, payload.Data.Any() ? payload.Data.Count : 0);

            return payload;
        }
    }
}
