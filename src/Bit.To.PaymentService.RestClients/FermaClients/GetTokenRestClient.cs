using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using Bit.To.PaymentService.Abstractions;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetTokenRestClient: IQueryHandler<GetToken, FermaAuthDto>
    {
        private readonly string _endpoint;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        public GetTokenRestClient(string endpoint)
        {
            _endpoint = endpoint;
        }
        public FermaAuthDto Execute(GetToken query)
        {
            var client = new RestClient(_endpoint);
            var request = new RestRequest(Method.POST);
            var jsonContent = JsonConvert.SerializeObject(query);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", jsonContent, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.StatusDescription);

            var fermaResponse = JsonConvert.DeserializeObject<FermaAuthResponse>(response.Content);

            if (!String.Equals(fermaResponse.Status, "Success", StringComparison.OrdinalIgnoreCase))
            {
                Log.ErrorFormat("... {error}", fermaResponse.Error);
                throw new Exception(fermaResponse.Status);
            }

            return new FermaAuthDto(fermaResponse.Data.AuthToken, fermaResponse.Data.ExpirationDateUtc);
        }
    }
}
