using System;
using System.Net;
using Bit.To.PaymentService.Abstractions;
using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.RestClients.FermaClients;
using Bit.To.PaymentService.RestClients.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Bit.To.PaymentService.RestClients
{
    public class CreateRecieptRestClient
    {
        private readonly string _endpoint;
        private readonly string _token;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public CreateRecieptRestClient(string endpoint, string token)
        {
            _endpoint = endpoint;
            _token = token;
        }

        public string Execute(CreateReceipt cmd)
        {
            var client = new RestClient(_endpoint);
            var request = new RestRequest(Method.POST);
            request.AddParameter("AuthToken", _token, ParameterType.QueryString);
            var jsonContent = JsonConvert.SerializeObject(cmd);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", jsonContent, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.StatusDescription);

            var fermaResponse = JsonConvert.DeserializeObject<CreateReceiptResponse>(response.Content);

            if (!String.Equals(fermaResponse.Status, "Success", StringComparison.OrdinalIgnoreCase))
            {
                Log.ErrorFormat("... {error}", fermaResponse.Error);
                throw new Exception(fermaResponse.Status);
            }

            return fermaResponse.Data.ReceiptId;
        }
    }
}
