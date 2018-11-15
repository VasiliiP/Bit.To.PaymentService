using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Models;

namespace Bit.To.PaymentService.Services
{
    public class FermaService : IFermaService
    {
        #region private
        private readonly string _login = "gollandec@yandex.ru";
        private readonly string _password = "jasoncey";
        private readonly string _baseUrl = "https://ofd.ru/";
        private readonly string _authResource = "api/Authorization/CreateAuthToken";
        private readonly string _inn = "2539112357";
        private FermaAuthData FermaAuthData { get; set; }

        private T ExecuteGet<T>(RestRequest request) where T : new()
        {

            request.AddParameter("AuthToken", GetToken(), ParameterType.UrlSegment);
            request.Method = Method.GET;
            request.AddHeader("Content-Type", "application/json");

            var client = new RestClient { BaseUrl = new System.Uri(_baseUrl) };
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
            return response.Data;
        }

        private T ExecutePost<T, K>(RestRequest request, K obj) where T : new()
        {
            request.AddParameter("AuthToken", GetToken(), ParameterType.UrlSegment);
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,

                Converters = new List<JsonConverter>
                {
                    new IsoDateTimeConverter()
                    {
                        DateTimeFormat= "yyyy-MM-ddTHH:mm:ss"
                    }
                }
            };

            var myContentJson = JsonConvert.SerializeObject(obj, settings);
            request.AddParameter("application/json", myContentJson, ParameterType.RequestBody);

            var client = new RestClient { BaseUrl = new System.Uri(_baseUrl) };
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                Log.Logger.Error("ExecutePost");
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _Log.Error("ExecutePost");
            }

            return response.Data;
        }

        /// <summary>
        /// Возвращает токен авторизации. Если токен пустой или просрочен метод получает новый
        /// </summary>
        private string GetToken()
        {
            if (FermaAuthData != null && FermaAuthData.IsValid)
                return FermaAuthData.AuthToken;

            var client = new RestClient { BaseUrl = new System.Uri(_baseUrl) };
            var loginRequest = new RestRequest(_authResource, Method.POST);
            var body = JsonConvert.SerializeObject(new
            {
                Login = _login,
                Password = _password
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
                Log.Error("GetToken()");
                //var sb = new StringBuilder();
                //foreach (var param in response.Request.Parameters)
                //{
                //    sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
                //}

                var exception = new ApplicationException("", null);
                throw exception;
            }

            FermaAuthData = response.Data;

            return FermaAuthData.AuthToken;
        }
        #endregion

        public RecieptResponse CreateReciept()
        {
            var payload = new RecieptRequest
            {
                Inn = _inn,
                InvoiceId = "InvoiceId100",
                LocalDate = DateTime.UtcNow,
                Type = "Income",
                CustomerReceipt = new CustomerReceipt
                {
                    Email = "asd@dsa.ru",
                    Phone = "89991234567",
                    TaxationSystem = "Common",
                    Items = new List<RecieptItem>
                    {
                        new RecieptItem
                        {
                            Label = "Tomates",
                            Quantity = 12.00f,
                            Price = 40.00M,
                            Amount = 480M
                        },
                        new RecieptItem
                        {
                            Label = "Cucumbers",
                            Quantity = 10.00f,
                            Price = 40.00M,
                            Amount = 400M
                        }
                    }
                }
            };

            var request = new RestRequest();
            request.Resource = "api/kkt/cloud/receipt";

            return ExecutePost<RecieptResponse, RecieptRequest>(request, payload);
        }

    }

    public interface IFermaService
    {
        RecieptResponse CreateReciept();
    }
}
