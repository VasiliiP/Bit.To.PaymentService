using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.RestClients;
using Bit.To.PaymentService.Services;
using Nancy;
using Nancy.Extensions;
using Nancy.Responses;
using Newtonsoft.Json;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Bit.To.PaymentService.Web
{
    public sealed class CreateReceiptModule : NancyModule
    {
        public CreateReceiptModule(IFermaService fermaService, IReceiptFactory receiptFactory, string modulePath = "/") : base(modulePath)
        {
            Post(
                "/receipt",
                param =>
                {
                    var cmd = CreateCmd(Context, param);

                    if (cmd == null)
                        return new Response().WithStatusCode(Nancy.HttpStatusCode.BadRequest);

                    var createRecieptHandler = fermaService.GetCreateRecieptHandler();
                    createRecieptHandler.Execute(cmd);

                    return new Response().WithStatusCode(Nancy.HttpStatusCode.OK);
                },
                null,
                nameof(CreateReceiptModule));

            Get(
                "/list",
                param =>
                {              
                    fermaService.GetList();
                    return new Response().WithStatusCode(Nancy.HttpStatusCode.OK);
                },
                null,
                nameof(CreateReceiptModule));
        }

        private static CreateReceipt CreateCmd(NancyContext context, dynamic param)
        {
            var cmd = CreateCmd(context);
            if (cmd == null)
                return null;

            return cmd;
        }

        private static CreateReceipt CreateCmd(NancyContext context)
        {
            var s = context.Request.Body.AsString();
            if (String.IsNullOrWhiteSpace(s)) return null;
            var cmd = JsonConvert.DeserializeObject<CreateReceipt>(s);
            return cmd;
        }
    }
}
