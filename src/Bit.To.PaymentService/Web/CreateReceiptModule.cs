using Bit.To.PaymentService.Abstractions.Commands;
using Bit.To.PaymentService.Services;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Bit.To.PaymentService.Abstractions.Models;
using Bit.To.PaymentService.Abstractions.Queries;

namespace Bit.To.PaymentService.Web
{
    public sealed class CreateReceiptModule : NancyModule
    {
        public CreateReceiptModule(ICommandHandler<CreateReceipt> createHandler, IQueryHandler<GetReceiptsList, List<ReceiptDto>> getListHandler, string modulePath = "/") : base(modulePath)
        {
            Post(
                "/receipt",
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
                nameof(CreateReceiptModule));

            Get(
                "/list",
                param =>
                {
                    try
                    {
                        var receipts = getListHandler.Execute(new GetReceiptsList{Request = new GetReceiptsListRequest { StartDateUtc = DateTime.Today.AddDays(-1) }});
                    }
                    catch (Exception e)
                    {
                        return new Response().WithStatusCode(HttpStatusCode.BadGateway);
                    }
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
