
![alt text](https://github.com/enisgurkann/EPAYMENT/blob/master/Epayment.PNG?raw=true)

# EPAYMENT -Multi Payment Provider for .Net Core

[![GitHub](https://img.shields.io/github/license/enisgurkann/EPAYMENT?color=594ae2&logo=github&style=flat-square)](https://github.com/enisgurkann/EPAYMENT/blob/master/LICENSE)
[![GitHub Repo stars](https://img.shields.io/github/stars/enisgurkann/EPAYMENT?color=594ae2&style=flat-square&logo=github)](https://github.com/enisgurkann/EPAYMENT/stargazers)
[![GitHub last commit](https://img.shields.io/github/last-commit/enisgurkann/EPAYMENT?color=594ae2&style=flat-square&logo=github)](https://github.com/mudblazor/mudblazor)
[![Contributors](https://img.shields.io/github/contributors/enisgurkann/EPAYMENT?color=594ae2&style=flat-square&logo=github)](https://github.com/enisgurkann/EPAYMENT/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/enisgurkann/EPAYMENT?color=594ae2&logo=github&style=flat-square)](https://github.com/enisgurkann/EPAYMENT/discussions)
[![Nuget version](https://img.shields.io/nuget/v/EPAYMENT?color=ff4081&label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/EPAYMENT/)
[![Nuget downloads](https://img.shields.io/nuget/dt/EPAYMENT?color=ff4081&label=nuget%20downloads&logo=nuget&style=flat-square)](https://www.nuget.org/packages/EPAYMENT/)



Tüm bankalarla uyumlu sanal pos servisi

Factory pattern structure written with .net5 for payment services

 ## BANK ENGINE
 ASSECO , FINANSBANK , VAKIFBANK , GARANTI , YAPIKREDI , PAYTR , PARAMPOS
 
 ## BANKS
 
 AKBANK FINANSBANK GARANTI VAKIFBANK ZIRAATBANKASI AKTIFBANK YAPIKREDI DENIZBANK İŞBANKASI HALKBANKASI 

## Payment Provider Usage

```
PM> Install-Package EPAYMENT
```

```csharp
  services.AddPaymentProvider();
 ```
 
 
```
PM> Injection
```


```csharp
  private readonly IPaymentProviderFactory _paymentProviderFactory;
  public SanalPosController(IPaymentProviderFactory paymentProviderFactory)
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


 
