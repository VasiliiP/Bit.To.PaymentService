using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Logging;
using Bit.To.PaymentService.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using Bit.To.PaymentService.RestClients;

namespace Bit.To.PaymentService.Services
{
    public class FermaService : IFermaService
    {
        private readonly string _fermaLogin;
        private readonly string _fermaPassword;
        private readonly string _fermaBaseUrl;
        private readonly string _authResource;
        private readonly string _inn;
        private readonly string _createReceiptResource;
        private FermaAuthData FermaAuthData { get; set; }
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        private readonly IReceiptFactory ReceiptFactory;

        public FermaService(IReceiptFactory receiptFactory, 
                            string fermaLogin, 
                            string fermaPassword, 
                            string fermaBaseUrl, 
                            string authResource, 
                            string createReceiptResource, 
                            string inn)
        {
            ReceiptFactory = receiptFactory;
            _fermaLogin = fermaLogin;
            _fermaPassword = fermaPassword;
            _fermaBaseUrl = fermaBaseUrl;
            _authResource = authResource;
            _createReceiptResource = createReceiptResource;
            _inn = inn;
        }

        /// <summary>
        /// Возвращает токен авторизации. Если токен пустой или просрочен метод получает новый
        /// </summary>
        private string GetToken()
        {
            if (FermaAuthData != null && FermaAuthData.IsValid)
                return FermaAuthData.AuthToken;

            var client = new RestClient { BaseUrl = new System.Uri(_fermaBaseUrl) };
            var loginRequest = new RestRequest(_authResource, Method.POST);
            var body = JsonConvert.SerializeObject(new
            {
                Login = _fermaLogin,
                Password = _fermaPassword
            });
            loginRequest.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute<FermaAuthData>(loginRequest);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response. Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("GetToken error");
                var exception = new ApplicationException("", null);
                throw exception;
            }

            FermaAuthData = response.Data;
            return FermaAuthData.AuthToken;
        }
       

        public void CreateReciept()
        {
            var cmd = ReceiptFactory.Create(_inn);
            var handler = new CreateRecieptRestClient(GetToken(), _fermaBaseUrl, _createReceiptResource);
            handler.Execute(cmd);
        }
    }
}
