
![alt text](https://github.com/enisgurkann/EPAYMENT/blob/master/Epayment.PNG?raw=true)

# EPAYMENT -Multi Payment Provider for .Net Standard


Tüm bankalarla uyumlu sanal pos servisi

Factory pattern structure written with .net5 for payment services

 ASSECO,FINANSBANK,VAKIFBANK,GARANTI,YAPIKREDI,PAYTR

## Payment Provider Usage

```
PM> Install-Package EPAYMENT
```

```csharp
  services.AddSingleton<IPaymentProviderFactory, PaymentProviderFactory>();
 ```
 
 
```
PM> Injection
```


```csharp
  private readonly IPaymentProviderFactory _paymentProviderFactory;
  public SmsController(IPaymentProviderFactory paymentProviderFactory)
  {
       _paymentProviderFactory = paymentProviderFactory;
  }
```

```
PM> Using
```
```csharp
     
     
	var _type = PosEngineType.ASSECO;
	var paymentProvider = _paymentProviderFactory.Create(_type);
	var paymentParameterResult = paymentProvider.GetPaymentParameters(new PaymentRequest()
	{
		OrderNumber = OrderNumber,
		Username = "Username",
		Password = "Password",
		ClientId = "ClientID",
		Email = "test@hotmail.com",
		Phone = "0000000",
		TotalAmount = 1,
		SuccessUrl = $"https://{HttpContext.Request.Host}/Payment/Success?OrderNumber={OrderNumber}",
		FailUrl = $"https://{HttpContext.Request.Host}/Payment/Fail?OrderNumber={OrderNumber}",
		CustomerIpAddress = HttpContext.Connection.RemoteIpAddress.ToString()
	});

 var paymentForm = _paymentProviderFactory.CreatePaymentForm(paymentParameterResult.Parameters, _config.GetValue<Uri>("Payment:PosType"));
    
```


 
