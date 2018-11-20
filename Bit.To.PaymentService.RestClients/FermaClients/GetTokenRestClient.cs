using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.Models;
using Bit.To.PaymentService.RestClients.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetTokenRestClient: BaseFermaRestClient, IQueryHandler<GetToken, FermaAuthResponse>
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        public GetTokenRestClient(string baseUrl, string resource):base(null, baseUrl, resource)
        {
        }
        public FermaAuthResponse Execute(GetToken query)
        {
            var response = ExecutePost<FermaAuthResponse, GetToken>(query);

            if (response == null)
                return null;

            var payload = response.Data;
            Log.DebugFormat("{0} Ferma response is: Status:{1}, AuthToken:{2}",
                response.Request.Resource, payload.Status, payload.Data.AuthToken);

            return payload;
        }
    }
}
