using System;
using System.Collections.Generic;
using System.Text;

namespace EPAYMENT
{
    public static class PaymentHelper
    {
        public static string GetCardBin(string CardNumber)
        {
            string BinNumber = "";
            CardNumber = CardNumber.Replace(" ", "").Trim();
            if (CardNumber.Length >= 6)
                BinNumber = CardNumber.Substring(0, 6);

            return BinNumber;
        }

        public static string GetMaskedNumber(string source)
        {
            StringBuilder sb = new StringBuilder(source);

            const int skipLeft = 6;
            const int skipRight = 4;

            int left = -1;

            for (int i = 0, c = 0; i < sb.Length; ++i)
            {
                if (Char.IsDigit(sb[i]))
                {
                    c += 1;

                    if (c > skipLeft)
                    {
                        left = i;

                        break;
                    }
                }
            }

            for (int i = sb.Length - 1, c = 0; i >= left; --i)
                if (Char.IsDigit(sb[i]))
                {
                    c += 1;

                    if (c > skipRight)
                        sb[i] = '*';
                }

            return sb.ToString().Replace(" ","").Trim();
        }

    }
}
