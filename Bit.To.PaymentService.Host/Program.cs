using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Bit.Persistence;
using Bit.To.PaymentService.Services;
using Bit.To.PaymentService.Web;
using Bit.Validation;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;
using Nancy.Responses;
using Nancy.Swagger;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Swagger.ObjectModel;
using Topshelf;
using Topshelf.LibLog;
using Topshelf.Nancy;

namespace Bit.To.PaymentService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new JsonConverter[] {new StringEnumConverter()}
            };

            var config = new AppConfigurationBuilder()
                .AddJsonFile("cfg\\default.json")
                .AddJsonFile("cfg\\app.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File(
                    Path.Combine(String.Format(config["LogPath"].AsString(), config["ServiceName"]),
                        $"{config["ServiceName"]}.txt"),
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var container = CreateContainer(config);

            void LogError(ILifetimeScope c, IPipelines pipelines) =>
                pipelines.OnError.AddItemToStartOfPipeline(
                    (ctx, ex) =>
                    {
                        Log.Logger.Error("NancyHost Exception {@ex}", ex);

                        switch (ex)
                        {
                            case ValidationException validationException:
                                return new TextResponse(ex.Message) {StatusCode = HttpStatusCode.UnprocessableEntity,};
                        }

                        return null;
                    });

            // ConfigurableAutofacBootstrapper реализован локально, т.к. его package не публикуется
            var bootstrapper = new ConfigurableAutofacBootstrapper(
                container,
                new List<Action<ILifetimeScope, IPipelines>> {LogError},
                typeof(CreateReceiptModule),
                typeof(SwaggerModule));

            InitSwagger(config);

            HostFactory.Run(x =>
            {
                x.UseLibLog();
                x.Service<BaseService>(
                    s =>
                    {
                        s.ConstructUsing(settings => new BaseService());
                        s.WhenStarted(service => service.Start());
                        s.WhenStopped(service => service.Stop());
                        s.WithNancyEndpoint(
                            x,
                            c =>
                            {
                                c.UseBootstrapper(bootstrapper);
                                var uri = new Uri(config["HostUrl"]);
                                {
                                    c.AddHost(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath);
                                    //TODO: без авторезервирования не всегда запускается, потом убрать
                                    c.ConfigureNancy(h => h.UrlReservations = new UrlReservations() { CreateAutomatically = true });
                                }
                                c.CreateUrlReservationsOnInstall();
                                c.OpenFirewallPortsOnInstall(config["ServiceName"]);
                            });
                    });
                x.StartAutomaticallyDelayed();
                x.SetServiceName(config["ServiceName"]);
                x.SetDisplayName(config["ServiceName"]);
                x.SetDescription($"{config["ServiceDescription"]} | {config["HostUrl"]}");
                x.RunAsNetworkService();
            });
        }

        private static void InitSwagger(IAppConfiguration config)
        {
            SwaggerMetadataProvider.SetInfo(
                config["SvcMetaTitle"],
                config["SvcMetaVer"],
                $"[ Base URL: {config["HostUrl"]} ]",
                new Contact {EmailAddress = config["SvcMetaContact"]});

            SwaggerConfig.ResourceListingPath = config["SwaggerResourceListingPath"];

            SwaggerTypeMapping.AddTypeMapping(typeof(DateTime), typeof(string));
            SwaggerTypeMapping.AddTypeMapping(typeof(DateTime?), typeof(string));
            SwaggerTypeMapping.AddTypeMapping(typeof(TimeSpan), typeof(string));
            SwaggerTypeMapping.AddTypeMapping(typeof(TimeSpan?), typeof(string));
            SwaggerTypeMapping.AddTypeMapping(typeof(Guid), typeof(string));
        }

        private static IContainer CreateContainer(IAppConfiguration config)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<FermaService>()
                .WithParameter("fermaLogin", config["FermaLogin"].AsString())
                .WithParameter("fermaPassword", config["FermaPassword"].AsString())
                .WithParameter("fermaBaseUrl", config["FermaBaseUrl"].AsString())
                .WithParameter("authResource", config["AuthResource"].AsString())
                .WithParameter("createReceiptResource", config["CreateReceiptResource"].AsString())
                .WithParameter("getReceiptStatusResource", config["GetReceiptStatusResource"].AsString())
                .WithParameter("getReceiptsListResourse", config["GetReceiptsListResourse"].AsString())
                .WithParameter("inn", config["Inn"].AsString())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<CreateReceiptModule>();
            builder.RegisterType<CreateReceiptModule>().AsImplementedInterfaces();
            builder.RegisterType<ReceiptFactory>().AsImplementedInterfaces();
            builder.RegisterType<SwaggerModule>();

            return builder.Build();
        }

        private class BaseService
        {
            public void Start()
            {
            }

            public void Stop()
            {
            }
        }
    }
}
