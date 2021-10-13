using EPAYMENT.Models;
using EPAYMENT.Providers;
using EPAYMENT.TEST.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace EPAYMENT.TEST
{
    public class FinansbankProviderTest
    {

        [Fact]
        public void PaymentProviderFactory_CreateFinansbankPaymentProvider()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();


            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var paymentProviderFactory = new PaymentProviderFactory(serviceProvider, httpClientFactory.Object);
            IPaymentProvider provider = paymentProviderFactory.Create(Models.Enums.PosEngineType.FINANSBANK);

            var paymentGatewayResult = provider.GetPaymentParameters(new PaymentRequest
            {
                BankUrl = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate",
                CardHolderName = "Enis Gürkan",
                CardNumber = "4309-5345-4803-4109",
                ExpireMonth = 12,
                ExpireYear = 21,
                CvvCode = "000",
                Installment = 1,
                TotalAmount = 1,
                CustomerIpAddress = "127.0.0.1",
                CurrencyIsoCode = "949",
                LanguageIsoCode = "tr",
                OrderNumber = Guid.NewGuid().ToString(),
                SuccessUrl = "http://www.google.com",
                FailUrl = "http://www.google.com",
            });

            Assert.True(paymentGatewayResult.Success);
        }
    
    }
}
