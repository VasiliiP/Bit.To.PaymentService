using Bit.To.PaymentService.Abstractions;
using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.Models;
using System;
using System.Collections.Generic;

namespace Bit.To.PaymentService.Services
{
    public class FermaService : IFermaService
    {
        private readonly string _fermaLogin;
        private readonly string _fermaPassword;
        private readonly IQueryHandler<GetToken, FermaAuthDto> _getTokenHandler;

        private FermaAuth FermaAuth { get; set; }

        public FermaService(string fermaLogin, 
                            string fermaPassword, 
                            IQueryHandler<GetToken, FermaAuthDto> getTokenHandler)
        {
            _fermaLogin = fermaLogin;
            _fermaPassword = fermaPassword;
            _getTokenHandler = getTokenHandler;
        }

        /// <summary>
        /// Возвращает токен авторизации. Если токен пустой или просрочен метод получает новый
        /// </summary>
        public string GetToken()
        {
            if (FermaAuth != null && FermaAuth.IsValid)
                return FermaAuth.AuthToken;

            var dto = _getTokenHandler.Execute(new GetToken{ Login = _fermaLogin, Password = _fermaPassword});
            FermaAuth = FermaAuth.CreateNew(dto.AuthToken, dto.ExpirationDateUtc);
            return FermaAuth.AuthToken;
        }

        public void CheckStatus(string id)
        {
            //проверка статуса созданного чека
            var getReceiptStatusQuery = new GetReceiptStatus
            {
                Request = new GetReceiptRequest { ReceiptId = id }
            };
            //var status = _getStatusHandler.Execute(getReceiptStatusQuery);
        }

    }
}
