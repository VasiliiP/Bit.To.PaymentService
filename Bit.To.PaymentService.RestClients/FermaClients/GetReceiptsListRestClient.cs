using System;
using System.Collections.Generic;
using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.Logging;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetReceiptsListRestClient : IQueryHandler<GetReceiptsList, List<ReceiptDto>>
    {
        private readonly string _endpoint;
        private readonly string _token;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public GetReceiptsListRestClient(string endpoint, string token)
        {
            _endpoint = endpoint;
            _token = token;
        }

        public List<ReceiptDto> Execute(GetReceiptsList query)
        {
            var client = new RestClient(_endpoint);
            var request = new RestRequest(Method.POST);
            request.AddParameter("AuthToken", _token, ParameterType.QueryString);
            var jsonContent = JsonConvert.SerializeObject(query);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", jsonContent, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.StatusDescription);

            var fermaResponse = JsonConvert.DeserializeObject<ReceiptsListResponse>(response.Content);

            if (!String.Equals(fermaResponse.Status, "Success", StringComparison.OrdinalIgnoreCase))
            {
                Log.ErrorFormat("... {error}", fermaResponse.Error);
                throw new Exception(fermaResponse.Status);
            }
            var data = fermaResponse.Data;
            var result = new List<ReceiptDto>();

            foreach (var r in data)
            {
                var cRes = r.Receipt.CustomerReceipt;
                var cahbox = r.Receipt.cashboxInfoHolder;
                var items = cRes.Items.Select(x => new ReceiptItemDto(x.Label, x.Price, x.Quantity, x.Amount, x.Vat)).ToList();
                result.Add(new ReceiptDto(r.ReceiptId, r.StatusCode, r.StatusName, r.StatusMessage, r.ModifiedDateUtc, r.ReceiptDateUtc, r.InvoiceId, r.Receipt.Inn, r.Receipt.Type, cahbox.DeviceId, cahbox.RNM, cahbox.ZN, cahbox.FN, cahbox.FDN, cahbox.FPD, cRes.TaxationSystem, cRes.Email, cRes.Phone, cRes.InstallmentPlace, cRes.InstallmentAddress, cRes.AutomaticDeviceNumber, items));
            }

            return result;
        }

    }
}
