using System;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Bit.To.YaKassa.Abstractions;
using Bit.To.YaKassa.RestClients.Logging;
using Bit.To.YaKassa.RestClients.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Bit.To.YaKassa.RestClients.YaKassaClients
{
    public class GetPaymentRestClient
    {
        private readonly string _endpoint;
        private readonly string _shopId;
        private readonly string _secret;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public GetPaymentRestClient(string endpoint, string shopId, string secret)
        {
            _endpoint = endpoint;
            _shopId = shopId;
            _secret = secret;
        }

        public PaymentDto Execute(GetPayment query)
        {
            var client = new RestClient(_endpoint);
            client.Authenticator = new HttpBasicAuthenticator(_shopId, _secret);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("payment_id", query.PaymentId);
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorResponse = JsonConvert.DeserializeObject<YaKassaErrorResponse>(response.Content);
                var errorMsg = (YaKassaErrorEnum)response.StatusCode + " " + errorResponse?.Parameter;
                Log.ErrorFormat("... {error}", errorMsg);
                throw new Exception(errorMsg);
            }

            var p = JsonConvert.DeserializeObject<GetPaymentResponse>(response.Content);

            var result = new PaymentDto(p.id, p.status, p.paid, p.amount.Value, p.amount.Currency, null, p.created_at,
                p.expires_at, p.description, p.test, p.payment_method.type, p.payment_method.saved,
                p.payment_method.card.first6, p.payment_method.card.last4, p.payment_method.card.expiry_month,
                p.payment_method.card.expiry_year, p.payment_method.card.card_type, p.payment_method.title);

            return result;
        }
    }
}
