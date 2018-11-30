using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients;
using Bit.To.PaymentService.RestClients.FermaClients;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Bit.To.PaymentService.Tests
{
    [TestFixture]
    public class FermaApiAccessTests
    {
        private static IAppConfiguration CreateConfiguration() =>
            new AppConfigurationBuilder()
                .AddJsonFile("..\\..\\cfg\\default.json")
                .Build();

        [Test]
        public void GetFermaToken_TokenReceived()
        {
            var config = CreateConfiguration();
            var c = CreateContainer(config);
            var restClient = c.Resolve<GetTokenRestClient>();
            var token = restClient.Execute(new GetToken());
            Assert.IsNotEmpty(token);
        }

        [Test]
        public void CreateReceipt_IdReceived()
        {
            var config = CreateConfiguration();
            var c = CreateContainer(config);
            var restClient = c.Resolve<CreateRecieptRestClient>();
            var receiptId = restClient.Execute(Factory.CreateReceipt());
            Assert.IsNotEmpty(receiptId);
        }

        [Test]
        public void GetListReceipts_ListReceived()
        {
            var config = CreateConfiguration();
            var c = CreateContainer(config);
            var restClient = c.Resolve<GetReceiptsListRestClient>();
            var list = restClient.Execute(new GetReceiptsList { Request = new GetReceiptsListRequest { StartDateUtc = DateTime.Today.AddDays(-1) } });
            Assert.IsTrue(list.Any());
        }

        [Test]
        public void GetStatus_StatusReceived()
        {
            var config = CreateConfiguration();
            var c = CreateContainer(config);
            var restClient = c.Resolve<GetReceiptStatusRestClient>();
            var status = restClient.Execute(new GetReceiptStatus{Request = new GetReceiptRequest { ReceiptId = "e47d048d-aa35-48ec-8b73-8c4d77246765" } });
            Assert.IsNotEmpty(status.StatusName);
        }

        private static IContainer CreateContainer(IAppConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GetTokenRestClient>()
                .AsSelf()
                .AsImplementedInterfaces()
                .WithParameter("fermaLogin", config["FermaLogin"].AsString())
                .WithParameter("fermaPassword", config["FermaPassword"].AsString())
                .WithParameter("endpoint", config["FermaAuthEndpoint"].AsString());

            builder.RegisterType<CreateRecieptRestClient>()
                .AsSelf()
                .WithParameter("endpoint", config["FermaReceiptEndpoint"].AsString())
                .AsImplementedInterfaces();

            builder.RegisterType<GetReceiptsListRestClient>()
                .AsSelf()
                .WithParameter("endpoint", config["FermaReceiptsListEndpoint"].AsString())
                .AsImplementedInterfaces();

            builder.RegisterType<GetReceiptStatusRestClient>()
                .AsSelf()
                .WithParameter("endpoint", config["FermaStatusEndpoint"].AsString())
                .AsImplementedInterfaces();

            return builder.Build();
        }

    }
}
