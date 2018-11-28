using System.IO;
using System.Reflection;
using Autofac;
using Bit.To.PaymentService.Abstractions.Queries;
using Bit.To.PaymentService.RestClients.FermaClients;
using NUnit.Framework;

namespace Bit.To.PaymentService.Tests
{
    [TestFixture]
    public class FermaApiAccessTests
    {
        private IAppConfiguration CreateConfiguration() =>
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

        private static IContainer CreateContainer(IAppConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GetTokenRestClient>().AsSelf().AsImplementedInterfaces()
                .WithParameter("baseUrl", config["FermaAuthEndpoint"].AsString());

            return builder.Build();
        }
    }
}