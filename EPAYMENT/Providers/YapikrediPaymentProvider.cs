using EPAYMENT.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;

namespace EPAYMENT.Providers
{
    /// <summary>
    /// Yapıkredi sanal pos işlemleri Vakıfbank ile benzer şekilde, girilen kart bilgisinin 3D doğrulamasını yapıp eğer sonuç başarılıysa banka sms sayfasına yönlendirme yapılmasını istiyor.
    /// Kart bilgisi 3D ödeme için uygun olması durumunda yönlendirilecek sayfa bilgisini bize xml içerisinde dönüyor
    /// </summary>
    public class YapikrediPaymentProvider : IPaymentProvider
    {
        private readonly HttpClient _client;

        public YapikrediPaymentProvider(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Add("charset", "utf-8");

        }

        public PaymentParameterResult GetPaymentParameters(PaymentRequest request)
        {
            var parameterResult = new PaymentParameterResult();

            //Test ortam bilgileri
            string MERCHANT_ID = request.ClientId;
            string TERMINAL_ID = request.Username;
            string POSNET_ID = request.ClientId;

            try
            {
                //kart numarasından çizgi ve boşlukları kaldırıyoruz
                string cardNumber = request.CardNumber.Replace("-", string.Empty);
                cardNumber = cardNumber.Replace(" ", string.Empty).Trim();

                //yapıkredi bankasında tutar bilgisinde nokta, virgül gibi değerler istenmiyor. 1.10 TL'lik işlem 110 olarak gönderilmeli. Yani tutarı 100 ile çarpabiliriz.
                string amount = (request.TotalAmount * 100).ToString("N");//virgülden sonraki sıfırlara gerek yok

                string requestXml = "<?xml version=\"1.0\" encoding=\"ISO-8859-9\"?>" +
                                        "<posnetRequest>" +
                                            $"<mid>{MERCHANT_ID}</mid>" +
                                            $"<tid>{TERMINAL_ID}</tid>" +
                                            "<oosRequestData>" +
                                                $"<posnetid>{POSNET_ID}</posnetid>" +
                                                $"<XID>{request.OrderNumber}</XID>" +
                                                $"<amount>{amount}</amount>" +
                                                $"<currencyCode>{GetCurrencyCode(request.CurrencyIsoCode.ToString())}</currencyCode>" +
                                                $"<installment>{string.Format("{0:00}", request.Installment)}</installment>" +
                                                "<tranType>Sale</tranType>" +
                                                $"<cardHolderName>{request.CardHolderName}</cardHolderName>" +
                                                $"<ccno>{cardNumber}</ccno>" +
                                                $"<expDate>{request.ExpireMonth}{request.ExpireYear}</expDate>" +
                                                $"<cvc>{request.CvvCode}</cvc>" +
                                            "</oosRequestData>" +
                                        "</posnetRequest>";

                var param = new Dictionary<string, string>();
                param.Add("xmldata", requestXml);

                var response = _client.PostAsync("https://setmpos.ykb.com/PosnetWebService/XML", new FormUrlEncodedContent(param)).Result;
                string responseContent = response.Content.ReadAsStringAsync().Result;

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseContent);
                var approvedNode = xmlDocument.SelectSingleNode("posnetResponse/approved");
                var respTextNode = xmlDocument.SelectSingleNode("posnetResponse/respText");
                if (approvedNode.InnerText != "1")
                {
                    parameterResult.ErrorMessage = respTextNode.InnerText;
                    return parameterResult;
                }

                var data1Node = xmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/data1");
                var data2Node = xmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/data2");
                var signNode = xmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/sign");

                var parameters = new Dictionary<string, object>();
                parameters.Add("posnetData", data1Node.InnerText);
                parameters.Add("posnetData2", data2Node.InnerText);
                parameters.Add("digest", signNode.InnerText);

                parameters.Add("mid", MERCHANT_ID);
                parameters.Add("posnetID", POSNET_ID);

                //Vade Farklı işlemler için kullanılacak olan kampanya kodunu belirler.
                //Üye İşyeri için tanımlı olan kampanya kodu, İşyeri Yönetici Ekranlarına giriş yapıldıktan sonra, Üye İşyeri bilgileri sayfasından öğrenilebilinir. 
                parameters.Add("vftCode", string.Empty);

                parameters.Add("merchantReturnURL", request.SuccessUrl);//geri dönüş adresi
                parameters.Add("lang", request.LanguageIsoCode);
                parameters.Add("url", string.Empty);//openANewWindow 1 olarak ayarlanırsa buraya gidilecek url verilmeli
                parameters.Add("openANewWindow", "0");//POST edilecek formun yeni bir sayfaya mı yoksa mevcut sayfayı mı yönlendirileceği
                parameters.Add("useJokerVadaa", "1");//yapıkredi kartlarında vadaa kullanılabilirse izin verir

                parameterResult.Parameters = parameters;
                parameterResult.PaymentUrl = new Uri("https://setmpos.ykb.com/3DSWebService/YKBPaymentService");
                parameterResult.Success = true;

            }
            catch (Exception ex)
            {
                parameterResult.Success = false;
                parameterResult.ErrorMessage = ex.ToString();
            }

            return parameterResult;
        }

        public PaymentResult GetPaymentResult(IFormCollection form)
        {
            throw new NotImplementedException();
        }

        private string GetCurrencyCode(string currencyIsoCode)
        {
            return null;
        }
    }
}