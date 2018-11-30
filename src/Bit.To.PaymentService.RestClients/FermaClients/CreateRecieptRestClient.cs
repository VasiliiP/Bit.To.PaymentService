using System;
using System.Net;
using Bit.To.PaymentService.Abstractions;
using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.FermaClients;
using Bit.To.PaymentService.RestClients.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Bit.To.PaymentService.RestClients
{
    public class CreateRecieptRestClient
    {
        private readonly string _endpoint;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        private readonly IQueryHandler<GetToken, string> _tokenHandler;

        public CreateRecieptRestClient(string endpoint, IQueryHandler<GetToken, string> tokenHandler)
        {
            _endpoint = endpoint;
            _tokenHandler = tokenHandler;
        }

        public string Execute(CreateReceipt cmd)
        {
            var client = new RestClient(_endpoint);
            var request = new RestRequest(Method.POST);
            var token = _tokenHandler.Execute(new GetToken());
            request.AddParameter("AuthToken", token, ParameterType.QueryString);
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
