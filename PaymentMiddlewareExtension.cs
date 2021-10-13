using EPAYMENT.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAYMENT
{
    public static class PaymentMiddlewareExtension
    {
        public static IApplicationBuilder UsePayment(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PaymentMiddleware>();
        }
    }
}
