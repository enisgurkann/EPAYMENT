
namespace EPAYMENT.Models
{
    public class PaymentRequest
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public string CvvCode { get; set; }
        public int Installment { get; set; }
        public double TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string LanguageIsoCode { get; set; }
        public string CustomerIpAddress { get; set; }


        public string BankUrl { get; set; }
        public string ClientId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string StoreKey { get; set; }

        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}