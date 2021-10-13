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
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();


            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var paymentProviderFactory = new PaymentProviderFactory(serviceProvider, httpClientFactory.Object);
            IPaymentProvider provider = paymentProviderFactory.Create(Models.Enums.PosEngineType.PAYTR);

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

            Assert.True(paymentGatewayResult.Success);
        }
    
    }
}
