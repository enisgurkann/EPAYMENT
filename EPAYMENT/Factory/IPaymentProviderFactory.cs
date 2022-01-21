using EPAYMENT.Models.Enums;
using System;
using System.Collections.Generic;

namespace EPAYMENT.Factory
{
    public interface IPaymentProviderFactory
    {
        IPaymentProvider Create(PosEngineType type);
        string CreatePaymentForm(IDictionary<string, object> parameters, Uri paymentUrl, bool appendFormSubmitScript = true);
    }
}