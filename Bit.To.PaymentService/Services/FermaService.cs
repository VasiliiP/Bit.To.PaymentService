using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Logging;
using Bit.To.PaymentService.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients;
using Bit.To.PaymentService.RestClients.FermaClients;

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
        private readonly string _getReceiptStatusResource;
        private readonly string _getReceiptsListResourse;
        private FermaAuth FermaAuth { get; set; }
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        private readonly IReceiptFactory ReceiptFactory;

        public FermaService(IReceiptFactory receiptFactory, 
                            string fermaLogin, 
                            string fermaPassword, 
                            string fermaBaseUrl, 
                            string authResource, 
                            string createReceiptResource, 
                            string inn, 
                            string getReceiptStatusResource, 
                            string getReceiptsListResourse)
        {
            ReceiptFactory = receiptFactory;
            _fermaLogin = fermaLogin;
            _fermaPassword = fermaPassword;
            _fermaBaseUrl = fermaBaseUrl;
            _authResource = authResource;
            _createReceiptResource = createReceiptResource;
            _inn = inn;
            _getReceiptStatusResource = getReceiptStatusResource;
            _getReceiptsListResourse = getReceiptsListResourse;
        }

        /// <summary>
        /// Возвращает токен авторизации. Если токен пустой или просрочен метод получает новый
        /// </summary>
        private string GetToken()
        {
            if (FermaAuth != null && FermaAuth.IsValid)
                return FermaAuth.Data.AuthToken;

            var client = new RestClient { BaseUrl = new System.Uri(_fermaBaseUrl) };
            var loginRequest = new RestRequest(_authResource, Method.POST);
            var body = JsonConvert.SerializeObject(new
            {
                Login = _fermaLogin,
                Password = _fermaPassword
            });
            loginRequest.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute<FermaAuth>(loginRequest);
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

            FermaAuth = response.Data;
            return FermaAuth.Data.AuthToken;
        }
       

        public void TestMethods()
        {
            //создание чека
            var createRecieptCommand = ReceiptFactory.Create(_inn);
            var createRecieptHandler = new CreateRecieptRestClient(GetToken(), _fermaBaseUrl, _createReceiptResource);
            createRecieptHandler.Execute(createRecieptCommand);

            //проверка статуса созданного чека
            var getReceiptStatusQuery = new GetReceiptStatus
            {
                Request = new Request {ReceiptId = createRecieptHandler.Response.Data.ReceiptId}
            };
            var getReceiptStatusHandler = new GetReceiptStatusRestClient(GetToken(), _fermaBaseUrl, _getReceiptStatusResource);
            var status = getReceiptStatusHandler.Execute(getReceiptStatusQuery);
        }

        public void GetList()
        {
            //Запрос на получение всех созданных чеков со вчерашнего дня
            var getReceiptsListQuery = new GetReceiptsList
            {
                Request = new GetReceiptsListRequest {StartDateUtc = DateTime.Today.AddDays(-1)}
            };
            var hadler = new GetReceiptsListRestClient(GetToken(), _fermaBaseUrl, _getReceiptsListResourse);
            hadler.Execute(getReceiptsListQuery);
        }
    }
}
