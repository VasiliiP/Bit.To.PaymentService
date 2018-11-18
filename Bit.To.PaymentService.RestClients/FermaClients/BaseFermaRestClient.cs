using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.RestClients.Logging;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class BaseFermaRestClient
    {
        private readonly string _token;
        private readonly string _baseUrl;
        private readonly string _resource;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        protected BaseFermaRestClient(string token, string baseUrl, string resource)
        {
            _token = token;
            _baseUrl = baseUrl;
            _resource = resource;
        }

        protected IRestResponse<TResult> ExecutePost<TResult, TCmd>(TCmd cmd) where TResult : BaseFermaResponse, new()
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
            var client = new RestClient(new System.Uri(_baseUrl));
            var response = client.Execute<TResult>(request);
            var payload = response.Data;
            if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK )
            {
                var parameters = new StringBuilder();
                foreach (var param in response.Request.Parameters)
                {
                    parameters.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
                }
                Log.ErrorFormat("Request to {0} failed with StatusCode: {1}, ErrorMessage: {2}",
                    response.Request.Resource, (int)response.StatusCode, payload.Error?.Message);
                Log.DebugFormat("{0} response is: Status:{1}, Error:{2}\r\n Parmeters:{3}",
                    response.Request.Resource, payload.Status, payload.Error?.Message, parameters);
                return null;
            }
            return response;
        }
    }
}
