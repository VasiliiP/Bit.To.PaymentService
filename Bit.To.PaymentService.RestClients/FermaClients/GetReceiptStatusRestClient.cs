﻿using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.Logging;
using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace Bit.To.PaymentService.RestClients.FermaClients
{
    public class GetReceiptStatusRestClient : IQueryHandler<GetReceiptStatus, ReceiptStatusDto>
    {
        private readonly string _endpoint;
        private readonly string _token;
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        public GetReceiptStatusRestClient(string endpoint, string token)
        {
            _endpoint = endpoint;
            _token = token;
        }

        public ReceiptStatusDto Execute(GetReceiptStatus query)
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

            var fermaResponse = JsonConvert.DeserializeObject<ReceiptStatusResponse>(response.Content);

            if (!String.Equals(fermaResponse.Status, "Success", StringComparison.OrdinalIgnoreCase))
            {
                Log.ErrorFormat("... {error}", fermaResponse.Error);
                throw new Exception(fermaResponse.Status);
            }

            var data = fermaResponse.Data;

            return new ReceiptStatusDto(data.StatusCode, data.StatusName, data.StatusMessage, data.ModifiedDateUtc, 
                                     data.ReceiptDateUtc, data.Device.DeviceId, data.Device.RNM, data.Device.ZN, 
                                     data.Device.FN, data.Device.FDN, data.Device.FPD);
        }
    }
}
