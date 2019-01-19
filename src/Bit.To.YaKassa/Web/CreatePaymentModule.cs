using System;
using Bit.To.YaKassa.Abstractions.Commands;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace Bit.To.YaKassa.Web
{
    public sealed class CreatePaymentModule: NancyModule
    {
        public CreatePaymentModule(ICommandHandler<CreatePayment> createHandler, string modulePath = "/") : base(modulePath)
        {
            Post(
                "/payment",
                param =>
                {
                    var cmd = CreateCmd(Context, param);
                    if (cmd == null)
                        return new Response().WithStatusCode(HttpStatusCode.BadRequest);

                    try
                    {
                        createHandler.Execute(cmd);
                    }
                    catch (Exception e)
                    {
                        return new Response().WithStatusCode(HttpStatusCode.BadGateway);
                    }
                    return new Response().WithStatusCode(HttpStatusCode.OK);
                },
                null,
                nameof(CreatePaymentModule));
        }

        private static CreatePayment CreateCmd(NancyContext context, dynamic param)
        {
            var cmd = CreateCmd(context);
            if (cmd == null)
                return null;

            return cmd;
        }

        private static CreatePayment CreateCmd(NancyContext context)
        {
            var s = context.Request.Body.AsString();
            if (String.IsNullOrWhiteSpace(s)) return null;
            var cmd = JsonConvert.DeserializeObject<CreatePayment>(s);
            return cmd;
        }

    }
}
