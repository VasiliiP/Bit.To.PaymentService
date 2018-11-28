using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients;
using Bit.To.PaymentService.RestClients.FermaClients;
using Bit.To.PaymentService.Services;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Bit.To.PaymentService.Tests
{
    [TestFixture]
    public class FermaApiAccessTests
    {
        private static IAppConfiguration CreateConfiguration() =>
            new AppConfigurationBuilder()
                .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "cfg", "default.json"))
                .Build();

        [Test]
        public void GetFermaToken_TokenReceived()
        {
            var config = CreateConfiguration();
            var c = CreateContainer(config);
            var restClient = c.Resolve<GetTokenRestClient>();
            var token = restClient.Execute(new GetToken { Login = config["FermaLogin"], Password = config["FermaPassword"] });
            Assert.IsNotEmpty(token.AuthToken);
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

            builder.RegisterType<GetTokenRestClient>().AsSelf().AsImplementedInterfaces()
                .WithParameter("endpoint", config["FermaAuthEndpoint"].AsString());

            builder.RegisterType<FermaService>()
                .WithParameter("fermaLogin", config["FermaLogin"].AsString())
                .WithParameter("fermaPassword", config["FermaPassword"].AsString())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(ctx =>
            {
                var token = ctx.Resolve<IFermaService>().GetToken();
                var endpoint = config["FermaReceiptEndpoint"].AsString();
                return new CreateRecieptRestClient(endpoint, token);
            }).AsSelf().AsImplementedInterfaces();

            builder.Register(ctx =>
            {
                var token = ctx.Resolve<IFermaService>().GetToken();
                var endpoint = config["FermaReceiptsListEndpoint"].AsString();
                return new GetReceiptsListRestClient(endpoint, token);
            }).AsSelf().AsImplementedInterfaces();

            builder.Register(ctx =>
            {
                var token = ctx.Resolve<IFermaService>().GetToken();
                var endpoint = config["FermaStatusEndpoint"].AsString();
                return new GetReceiptStatusRestClient(endpoint, token);
            }).AsSelf().AsImplementedInterfaces();

            return builder.Build();
        }

    }
}
