using Bit.To.YaKassa.Abstractions.Commands;
using Bit.To.YaKassa.RestClients.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using Bit.To.YaKassa.RestClients.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Bit.To.YaKassa.RestClients.YaKassaClients
{
    public class CreatePaymentRestClient
    {
        private readonly string _endpoint;
        private readonly string _shopId;
        private readonly string _secret;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public CreatePaymentRestClient(string endpoint, string shopId, string secret)
        {
            _endpoint = endpoint;
            _shopId = shopId;
            _secret = secret;
        }

        public string Execute(CreatePayment cmd)
        {
            var client = new RestClient(_endpoint);
            client.Authenticator = new HttpBasicAuthenticator(_shopId, _secret);
            var request = new RestRequest(Method.POST);
            var jsonContent = JsonConvert.SerializeObject(cmd);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("Idempotence-Key", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("application/json", jsonContent, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorResponse = JsonConvert.DeserializeObject<YaKassaErrorResponse>(response.Content);
                var errorMsg = (YaKassaErrorEnum)response.StatusCode + " " + errorResponse?.Parameter;
                Log.ErrorFormat("... {error}", errorMsg);
                throw new Exception(errorMsg);
            }

            var createPaymentResponse = JsonConvert.DeserializeObject<CreatePaymentResponse>(response.Content);
            return createPaymentResponse.Id;
        }
    }
}
