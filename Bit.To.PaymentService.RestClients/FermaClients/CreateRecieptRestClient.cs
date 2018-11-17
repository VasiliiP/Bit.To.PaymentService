using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.RestClients.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Bit.To.PaymentService.RestClients
{
    public class CreateRecieptRestClient : ICommandHandler<CreateReceipt>
    {
        private readonly string _token;
        private readonly string _baseUrl;
        private readonly string _resource;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public CreateRecieptRestClient(string token, string baseUrl, string resource)
        {
            _token = token;
            _baseUrl = baseUrl;
            _resource = resource;
        }

        public void Execute(CreateReceipt cmd)
        {
            var request = new RestRequest { Resource = _resource };
            request.AddParameter("AuthToken", _token, ParameterType.QueryString);
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                Converters = new List<JsonConverter> { new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss" } }
            };
            var myContentJson = JsonConvert.SerializeObject(cmd, settings);
            request.AddParameter("application/json", myContentJson, ParameterType.RequestBody);

            var client = new RestClient { BaseUrl = new System.Uri(_baseUrl) };
            var response = client.Execute<ReceiptResponse>(request);

            if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK)
            {
                var sb = new StringBuilder();
                foreach (var param in response.Request.Parameters)
                {
                    sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
                }
                Log.ErrorFormat("Response {0} failed with status {1}", sb.ToString(), (int)response.StatusCode);
                string message = response.StatusCode.ToString() + sb;
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
            var result = response.Data;
            Log.DebugFormat("CreateReceipt response is: Status:{0}, Erros:{1}", result.Status, string.Join(";\r\n", result.Errors));
            
        }

    }
}
