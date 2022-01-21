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
    public class PayTRProviderTest
    {

        [Fact]
        public void PaymentProviderFactory_CreatePayTRPaymentProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();

            var httpClientFactory = new Mock<IHttpClientFactory>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var paymentProviderFactory = new Factory.PaymentProviderFactory(serviceProvider, httpClientFactory.Object);
            var provider = paymentProviderFactory.Create(Models.Enums.PosEngineType.PAYTR);

            var paymentGatewayResult = provider.GetPaymentParameters(new PaymentRequest
            {
                BankUrl = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate",
                Email = "test@hotmail.com",
                StoreKey = "123123123",
                ClientId = "123",
                Password = "123",
                Phone = "00000000000",
                Username = "testuser",
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

            Assert.Equal(paymentGatewayResult.ErrorMessage, "PAYTR IFRAME failed. reason:Gecersiz istek veya magaza aktif degil");
        }
    
    }
}
