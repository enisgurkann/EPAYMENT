using EPAYMENT.Models.Enums;
using EPAYMENT.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace EPAYMENT.Factory
{
    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentProviderFactory(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
        }

        public IPaymentProvider Create(PosEngineType type)
        {
            switch (type)
            {
                case PosEngineType.ASSECO:
                    return ActivatorUtilities.GetServiceOrCreateInstance<AssecoPaymentProvider>(_serviceProvider);
                case PosEngineType.FINANSBANK:
                    return ActivatorUtilities.GetServiceOrCreateInstance<FinansbankPaymentProvider>(_serviceProvider);
                case PosEngineType.YAPIKREDI:
                    return ActivatorUtilities.GetServiceOrCreateInstance<YapikrediPaymentProvider>(_serviceProvider);
                case PosEngineType.GARANTI:
                    return ActivatorUtilities.GetServiceOrCreateInstance<GarantiPaymentProvider>(_serviceProvider);
                case PosEngineType.PAYTR:
                    return ActivatorUtilities.GetServiceOrCreateInstance<PayTRProvider>(_serviceProvider);
                default:
                    throw new NotSupportedException("Bank engine not supported");
            }
        }

        public string CreatePaymentForm(IDictionary<string, object> parameters, Uri paymentUrl, bool appendSubmitScript = true)
        {
            if (parameters == null || !parameters.Any())
                throw new ArgumentNullException(nameof(parameters));

            if (paymentUrl == null)
                throw new ArgumentNullException(nameof(paymentUrl));

            var formId = "PaymentForm";
            var formBuilder = new StringBuilder();
            formBuilder.Append($"<form id=\"{formId}\" name=\"{formId}\" action=\"{paymentUrl}\" role=\"form\" method=\"POST\">");
            foreach (var parameter in parameters)
            {
                formBuilder.Append($"<input type=\"hidden\" name=\"{parameter.Key}\" value=\"{parameter.Value}\">");
            }
            formBuilder.Append("</form>");

            if (appendSubmitScript)
            {
                var scriptBuilder = new StringBuilder();
                scriptBuilder.Append("<script>");
                scriptBuilder.Append($"document.{formId}.submit();");
                scriptBuilder.Append("</script>");
                formBuilder.Append(scriptBuilder.ToString());
            }

            return formBuilder.ToString();
        }
    }
}
