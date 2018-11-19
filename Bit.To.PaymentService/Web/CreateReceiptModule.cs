using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Services;
using Nancy;
using Nancy.Responses;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Bit.To.PaymentService.Web
{
    public sealed class CreateReceiptModule : NancyModule
    {
        public CreateReceiptModule(IFermaService fermaService, string modulePath = "/") : base(modulePath)
        {
            Get(
                "/receipt",
                param =>
                {
                    fermaService.TestMethods();
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
    }
}
