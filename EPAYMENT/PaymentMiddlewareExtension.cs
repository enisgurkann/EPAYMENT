using EPAYMENT.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EPAYMENT
{
    public static class PaymentMiddlewareExtension
    {
        public static IMvcCoreBuilder AddPaymentProvider(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var builder = services.AddMvcCore();
            builder.Services.AddTransient<IPaymentProviderFactory, PaymentProviderFactory>();

            return builder;
        }
        public static IMvcCoreBuilder AddPaymentProvider(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            var builder = services.AddPaymentProvider();
            builder.Services.Configure(setupAction);

            return builder;
        }
    }
}
