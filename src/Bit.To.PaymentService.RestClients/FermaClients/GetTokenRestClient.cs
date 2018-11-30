using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using Bit.To.PaymentService.Abstractions;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetTokenRestClient: IQueryHandler<GetToken, string>
    {
        private readonly string _endpoint;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        private static string _token;
        private static DateTime _expirationDate;
        private string _fermaLogin;
        private string _fermaPassword;

        public GetTokenRestClient(string endpoint, string fermaLogin, string fermaPassword)
        {
            _fermaLogin = fermaLogin;
            _fermaPassword = fermaPassword;
            _endpoint = endpoint;
        }
        public string Execute(GetToken emptyQuery)
        {
            if (!string.IsNullOrEmpty(_token) && _expirationDate > DateTime.UtcNow)
                return _token;

            var query = new GetToken {Login = _fermaLogin, Password = _fermaPassword};
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
            _token = fermaResponse.Data.AuthToken;
            _expirationDate = fermaResponse.Data.ExpirationDateUtc;

            return _token;
        }
    }
}
