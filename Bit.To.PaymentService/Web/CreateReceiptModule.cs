using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bit.To.PaymentService.Services;
using Nancy;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Bit.To.PaymentService.Web
{
    public sealed class CreateReceiptModule : NancyModule
    {
        public CreateReceiptModule(IFermaService fermaService, string modulePath = "/receipt") : base(modulePath)
        {
            Get(
                "/",
                param =>
                {
                    fermaService.CreateReciept();
                    return new Response().StatusCode == Nancy.HttpStatusCode.OK;
                },
                null,
                nameof(CreateReceiptModule));
        }
    }
}
