using Bit.To.YaKassa.RestClients.YaKassaClients;
using NUnit.Framework;
using Autofac;
using Assert = NUnit.Framework.Assert;

namespace Bit.To.YaKassa.Tests
{
    [TestFixture]
    public class YaKassaApiAccessTests
    {
        private static IAppConfiguration CreateConfiguration() =>
            new AppConfigurationBuilder()
                .AddJsonFile("..\\..\\cfg\\default.json")
                .Build();
        [Test]
        public void CreatePayment_IdReceived()
        {
            var config = CreateConfiguration();
            var c = CreateContainer(config);
            var restClient = c.Resolve<CreatePaymentRestClient>();
            var receiptId = restClient.Execute(YaKassaEntityFactory.CreatePayment());
            Assert.IsNotEmpty(receiptId);
        }

        private static IContainer CreateContainer(IAppConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CreatePaymentRestClient>()
                .AsSelf()
                .WithParameter("endpoint", config["YaKassaCreatePaymentEndpoint"].AsString())
                .WithParameter("shopId", config["YaKassaShopId"].AsString())
                .WithParameter("secret", config["YaKassaSecret"].AsString())
                .AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
