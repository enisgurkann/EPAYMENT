using EPAYMENT.Models;
using Microsoft.AspNetCore.Http;
namespace EPAYMENT
{
    public interface IPaymentProvider
    {
        PaymentParameterResult GetPaymentParameters(PaymentRequest request);
        PaymentResult GetPaymentResult(IFormCollection form);
    }
}