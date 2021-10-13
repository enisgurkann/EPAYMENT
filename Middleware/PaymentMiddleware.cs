using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAYMENT.Middleware
{
    public class PaymentMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IPaymentProviderFactory _paymentProviderFactory;
        public PaymentMiddleware(RequestDelegate next, PaymentProviderFactory paymentProviderFactory)
        {
            _next = next;
            _paymentProviderFactory = paymentProviderFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}
