using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.YaKassa.Abstractions.Commands;
using Bit.To.YaKassa.RestClients.YaKassaClients;

namespace Bit.To.YaKassa.CommandHandlers
{
    public class CreatePaymentHandler : ICommandHandler<CreatePayment>
    {
        private readonly string _endpoint;
        private readonly string _shopId;
        private readonly string _secret;

        public CreatePaymentHandler(string endpoint, string shopId, string secret)
        {
            _endpoint = endpoint;
            _shopId = shopId;
            _secret = secret;
        }

        public void Execute(CreatePayment cmd)
        {
            var restClient = new CreatePaymentRestClient(_endpoint, _shopId, _secret);
            var receiptId = restClient.Execute(cmd);
            //_repository.Save(cmd, new Guid(receiptId));
        }
    }
}
