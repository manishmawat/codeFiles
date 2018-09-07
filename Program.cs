using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TELoIP
{
    class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    /// <summary>
    /// Provides the ability to generate various card numbers.
    /// Generally used for testing but has the ability to generate valid 
    /// rewards card numbers.
    /// </summary>
    public static class CardNumberGenerator
    {
        #region constraint region

        /// <summary>
        /// Prefix lists for American Express card numbers.
        /// </summary>
        private static readonly string[] AmexPrefixList = { "34", "37" };

        /// <summary>
        /// Prefix list for Diner's card numbers.
        /// </summary>
        private static readonly string[] DinersPrefixList = { "300", "301", "302", "303", "36", "38" };

        /// <summary>
        /// Prefix list for Discover card numbers.
        /// </summary>
        private static readonly string[] DiscoverPrefixList = { "6011" };

        /// <summary>
        /// Prefix list for ENROUTE card numbers.
        /// </summary>
        private static readonly string[] EnroutePrefixList = { "2014", "2149" };

        /// <summary>
        /// Prefix list for JCB 15 card numbers.
        /// </summary>
        private static readonly string[] Jcb15PrefixList = { "2100", "1800" };

        /// <summary>
        /// Prefix list of JCB 16 card numbers.
        /// </summary>
        private static readonly string[] Jcb16PrefixList = { "3088", "3096", "3112", "3158", "3337", "3528" };

        /// <summary>
        /// Prefix list for Mastercard numbers.
        /// </summary>
        private static readonly string[] MastercardPrefixList = { "51", "52", "53", "54", "55" };

        /// <summary>
        /// Prefix list for Visa card numbers.
        /// </summary>
        private static readonly string[] VisaPrefixList = { "4539", "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4" };

        /// <summary>
        /// Prefix list for Voyager card numbers.
        /// </summary>
        private static readonly string[] VoyagerPrefixList = { "8699" };

        /// <summary>
        /// Prefix list for TGN card numbers.
        /// </summary>
        /// <remarks>
        /// We keep these for historical purposes.
        /// </remarks>
        private static readonly string[] TgnPrefixList = { "99", "9999" };

        /// <summary>
        /// Prefix list for TGN promotional card numbers.
        /// </summary>
        /// <remarks>
        /// We keep these for historical purposes.
        /// </remarks>
        private static readonly string[] TgnPromoPrefixList = { "11" };

        /// <summary>
        /// Prefix list for Rewards card numbers.
        /// </summary>
        private static readonly string[] RewardsPrefixList = { "22", "2222" };

        #endregion

        /// <summary>
        /// Returns the issuer of a card using its first four digits
        /// </summary>
        /// <param name="cardPrefix">
        /// The first four digits of a card number.
        /// </param>
        /// <returns>The card issuer name for a specified card if found. Returns Unknown, if not found.</returns>
        public static Constants.CardIssuer GetCardIssuerNameByBin(string cardPrefix)
        {
            var firstTwoChars = cardPrefix.Substring(0, 2);
            var firstThreeChars = cardPrefix.Substring(0, 3);

            if (AmexPrefixList.Contains(firstTwoChars))
            {
                return Constants.CardIssuer.AmericanExpress;
            }

            if (DinersPrefixList.Contains(firstThreeChars) || DinersPrefixList.Contains(firstTwoChars))
            {
                return Constants.CardIssuer.Diners;
            }

            if (DiscoverPrefixList.Contains(cardPrefix))
            {
                return Constants.CardIssuer.Discover;
            }

            if (EnroutePrefixList.Contains(cardPrefix))
            {
                return Constants.CardIssuer.Enroute;
            }

            if (Jcb15PrefixList.Contains(cardPrefix) || Jcb16PrefixList.Contains(cardPrefix))
            {
                return Constants.CardIssuer.Jcb;
            }

            if (MastercardPrefixList.Contains(firstTwoChars))
            {
                return Constants.CardIssuer.Mastercard;
            }

            if (VisaPrefixList.Contains(cardPrefix) || cardPrefix.Substring(0, 1) == "4")
            {
                return Constants.CardIssuer.Visa;
            }

            if (VoyagerPrefixList.Contains(cardPrefix))
            {
                return Constants.CardIssuer.Voyager;
            }

            if (TgnPromoPrefixList.Contains(firstTwoChars))
            {
                return Constants.CardIssuer.TgnPromoCard;
            }

            if (TgnPrefixList.Contains(firstTwoChars) || TgnPrefixList.Contains(cardPrefix))
            {
                return Constants.CardIssuer.TgnGiftCard;
            }

            if (RewardsPrefixList.Contains(firstTwoChars) || RewardsPrefixList.Contains(cardPrefix))
            {
                return Constants.CardIssuer.Rewards;
            }

            return Constants.CardIssuer.Unknown;
        }

        /// <summary>
        /// Checks if a specified card number is a rewards card number.
        /// </summary>
        /// <param name="cardNumber">The card number to be checked.</param>
        /// <returns>
        /// True if the specified card number is a rewards card number, 
        /// false otherwise.
        /// </returns>
        public static bool IsRewardsCardNumber(string cardNumber)
        {
            if (cardNumber.Length < 4)
            {
                throw new ApplicationException("Invalid card number.");
            }

            var cardPrefix = cardNumber.Length > 4 ? cardNumber.Substring(0, 4) : cardNumber;

            return GetCardIssuerNameByBin(cardPrefix) == Constants.CardIssuer.Rewards;
        }

        /// <summary>
        /// Generates a Visa card number.
        /// </summary>
        /// <param name="random">
        /// An instance of <see cref="System.Random"/>.
        /// </param>
        /// <returns>
        /// The generated card number.
        /// </returns>
        public static string GenerateVisaCardNumber()
        {
            return GetCreditCardNumbers(VisaPrefixList, 16, 1).First();
        }

        /// <summary>
        /// Generates a Rewards card number.
        /// </summary>        
        /// <returns>
        /// The generated card number.
        /// </returns>
        public static string GenerateRewardsCardNumber()
        {
            return GetCreditCardNumbers(RewardsPrefixList, 16, 1).First();
        }

        /// <summary>
        /// Validates whether a given credit card number passes the MOD 10 check.
        /// </summary>
        /// <param name="creditCardNumber">
        /// The credit card number.
        /// </param>
        /// <returns>
        /// True when valid, false otherwise.
        /// </returns>
        public static bool IsValidCreditCardNumber(string creditCardNumber)
        {
            try
            {
                var reversedNumber = creditCardNumber.ToCharArray().Reverse().ToList();

                var mod10Count = 0;
                for (var i = 0; i < reversedNumber.Count(); i++)
                {
                    var augend = Convert.ToInt32(reversedNumber.ElementAt(i).ToString(CultureInfo.InvariantCulture));

                    if (((i + 1) % 2) == 0)
                    {
                        var productstring = (augend * 2).ToString(CultureInfo.InvariantCulture);
                        augend = productstring.Select((t, j) => Convert.ToInt32(productstring.ElementAt(j).ToString(CultureInfo.InvariantCulture))).Sum();
                    }

                    mod10Count += augend;
                }

                if ((mod10Count % 10) == 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// The get credit card numbers.
        /// </summary>
        /// <param name="prefixList">
        /// The prefix list.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <param name="howMany">
        /// The how many.
        /// </param>        
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<string> GetCreditCardNumbers(IList<string> prefixList, int length, int howMany)
        {
            //var random = new Random();

            var result = new Stack<string>();

            var intRandom = new Random();

            for (int i = 0; i < howMany; i++)
            {
                int randomPrefix = intRandom.Next(0, prefixList.Count - 1);

                if (randomPrefix > 1)
                {
                    randomPrefix--;
                }

                string ccnumber = prefixList[randomPrefix];

                result.Push(CreateFakeCreditCardNumber(ccnumber, length/*, random*/));
            }

            return result;
        }

        /// <summary>
        /// Generates a card number based on the specifications provided.
        /// </summary>
        /// <param name="prefix">
        /// the start of the CC number as a string, any number
        ///  private of digits
        /// </param>
        /// <param name="length">
        /// length of the CC number to generate.
        /// * Typically 13 or 16
        /// </param>        
        /// <returns>
        /// The generated card number.
        /// </returns>
        private static string CreateFakeCreditCardNumber(string prefix, int length)
        {
            var random = new Random();

            var ccnumber = prefix;

            while (ccnumber.Length < (length - 1))
            {
                var rnd = (random.NextDouble() * 1.0f - 0f);

                ccnumber += Math.Floor(rnd * 10);
            }

            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();

            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString(CultureInfo.InvariantCulture)));

            // calculate sum
            var sum = 0;
            var pos = 0;
            var reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                var odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                {
                    odd -= 9;
                }

                sum += odd;

                if (pos != (length - 2))
                {
                    sum += reversedCCnumber[pos + 1];
                }

                pos += 2;
            }

            // calculate check digit
            var checkdigit = Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;
        }

    }
}
/// <summary>
/// Namespace for constant
/// </summary>
namespace Constants
{
    /// <summary>
    /// enum to define all the card issuer.
    /// </summary>
    public enum CardIssuer
    {
        AmericanExpress,
        Diners,
        Discover,
        Enroute,
        Jcb,
        Mastercard,
        Visa,
        Voyager,
        TgnPromoCard,
        TgnGiftCard,
        Rewards,
        Unknown
    }
}