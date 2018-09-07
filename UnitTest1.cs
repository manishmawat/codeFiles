using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TELoIP;

/// <summary>
/// 
/// </summary>
namespace TELoIPTest
{
    /// <summary>
    /// Test class
    /// </summary>
    [TestClass]
    public class TELoIP_Test
    {
        #region GetCardIssuerNameByBin
        /// <summary>
        /// Exception cases
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { CardNumberGenerator.GetCardIssuerNameByBin("21"); });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { CardNumberGenerator.GetCardIssuerNameByBin("1"); });
        }
        /// <summary>
        /// "3443", "3743", "2243"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForAmex()
        {
            Assert.AreEqual(Constants.CardIssuer.AmericanExpress,CardNumberGenerator.GetCardIssuerNameByBin("3443"));
            Assert.AreEqual(Constants.CardIssuer.AmericanExpress, CardNumberGenerator.GetCardIssuerNameByBin("3743"));
            Assert.AreNotEqual(Constants.CardIssuer.AmericanExpress, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
        }

        /// <summary>
        /// Test case for Diners
        /// "300", "301", "302", "303", "36", "38"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForDiners()
        {
            Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3003"));
            Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3013"));
            Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3023"));
            Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3033"));
            Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3634"));
            Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3823"));
            //Assert.AreEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("38231532546784356"));
            Assert.AreNotEqual(Constants.CardIssuer.Diners, CardNumberGenerator.GetCardIssuerNameByBin("3423"));
        }
        /// <summary>
        /// 6011
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForDiscove()
        {
            Assert.AreEqual(Constants.CardIssuer.Discover, CardNumberGenerator.GetCardIssuerNameByBin("6011"));
            //Assert.AreEqual(Constants.CardIssuer.Discover, CardNumberGenerator.GetCardIssuerNameByBin("6011"));
            Assert.AreNotEqual(Constants.CardIssuer.Discover, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
        }

        /// <summary>
        /// "2014", "2149"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForEnroute()
        {
            Assert.AreEqual(Constants.CardIssuer.Enroute, CardNumberGenerator.GetCardIssuerNameByBin("2014"));
            Assert.AreEqual(Constants.CardIssuer.Enroute, CardNumberGenerator.GetCardIssuerNameByBin("2149"));
            Assert.AreNotEqual(Constants.CardIssuer.Enroute, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
        }

        /// <summary>
        /// "2100", "1800", "3088", "3096", "3112", "3158", "3337", "3528"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForJcb()
        {
            // 15 digit
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("2100"));
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("1800"));
            Assert.AreNotEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("1200"));

            //16 Digit
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("2100"));
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("1800"));
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("2100"));
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("1800"));
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("2100"));
            Assert.AreEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("1800"));
            Assert.AreNotEqual(Constants.CardIssuer.Jcb, CardNumberGenerator.GetCardIssuerNameByBin("1200"));
        }

        /// <summary>
        /// "51", "52", "53", "54", "55"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForMaster()
        {
            // 15 digit
            Assert.AreEqual(Constants.CardIssuer.Mastercard, CardNumberGenerator.GetCardIssuerNameByBin("5100"));
            Assert.AreEqual(Constants.CardIssuer.Mastercard, CardNumberGenerator.GetCardIssuerNameByBin("5200"));
            Assert.AreEqual(Constants.CardIssuer.Mastercard, CardNumberGenerator.GetCardIssuerNameByBin("5300"));
            Assert.AreEqual(Constants.CardIssuer.Mastercard, CardNumberGenerator.GetCardIssuerNameByBin("5400"));
            Assert.AreEqual(Constants.CardIssuer.Mastercard, CardNumberGenerator.GetCardIssuerNameByBin("5500"));
            Assert.AreNotEqual(Constants.CardIssuer.Mastercard, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
        }

        /// <summary>
        /// "4539", "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForVisa()
        {
            // 15 digit
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4539"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4556"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4916"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4532"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4929"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("40240071"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4485"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4716"));
            Assert.AreEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("4000"));
            Assert.AreNotEqual(Constants.CardIssuer.Visa, CardNumberGenerator.GetCardIssuerNameByBin("1234"));
        }

        /// <summary>
        /// 8699
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForVoyager()
        {
            Assert.AreEqual(Constants.CardIssuer.Voyager, CardNumberGenerator.GetCardIssuerNameByBin("8699"));
            Assert.AreNotEqual(Constants.CardIssuer.Voyager, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
        }
        /// <summary>
        /// "11"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForTgnPromo()
        {
            Assert.AreEqual(Constants.CardIssuer.TgnPromoCard, CardNumberGenerator.GetCardIssuerNameByBin("1121"));
            Assert.AreNotEqual(Constants.CardIssuer.TgnPromoCard, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
        }
        /// <summary>
        /// "99", "9999"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForTgn()
        {
            Assert.AreEqual(Constants.CardIssuer.TgnGiftCard, CardNumberGenerator.GetCardIssuerNameByBin("9921"));
            Assert.AreEqual(Constants.CardIssuer.TgnGiftCard, CardNumberGenerator.GetCardIssuerNameByBin("9999"));
            Assert.AreNotEqual(Constants.CardIssuer.TgnGiftCard, CardNumberGenerator.GetCardIssuerNameByBin("9212"));
        }
        /// <summary>
        /// "22", "2222"
        /// </summary>
        [TestMethod]
        public void TestGetCardIssuerNameByBinForRewards()
        {
            Assert.AreEqual(Constants.CardIssuer.Rewards, CardNumberGenerator.GetCardIssuerNameByBin("2243"));
            Assert.AreEqual(Constants.CardIssuer.Rewards, CardNumberGenerator.GetCardIssuerNameByBin("2222"));
            Assert.AreNotEqual(Constants.CardIssuer.Rewards, CardNumberGenerator.GetCardIssuerNameByBin("9212"));
        }

        [TestMethod]
        public void TestGetCardIssuerNameByBinForUnknown()
        {
            Assert.AreEqual(Constants.CardIssuer.Unknown, CardNumberGenerator.GetCardIssuerNameByBin("1213"));
            Assert.AreNotEqual(Constants.CardIssuer.Unknown, CardNumberGenerator.GetCardIssuerNameByBin("2222"));
        }

        #endregion

        [TestMethod]
        public void TestIsRewardsCardNumberException()
        {
            Assert.ThrowsException<ApplicationException>(() => CardNumberGenerator.IsRewardsCardNumber("12"), "Invalid card number.");
            Assert.ThrowsException<ApplicationException>(() => CardNumberGenerator.IsRewardsCardNumber("22"), "Invalid card number.");
        }

        /// <summary>
        /// "22", "2222"
        /// </summary>
        [TestMethod]
        public void TestIsRewardsCardNumber()
        {
            Assert.AreEqual(true, CardNumberGenerator.IsRewardsCardNumber("2212"));
            Assert.AreEqual(true, CardNumberGenerator.IsRewardsCardNumber("2222"));
            Assert.AreEqual(true, CardNumberGenerator.IsRewardsCardNumber("2222123476589432"));
            Assert.AreNotEqual(true, CardNumberGenerator.IsRewardsCardNumber("2122123476589432"));
        }

        /// <summary>
        /// "4539", "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4"
        /// </summary>
        [TestMethod]
        public void TestGenerateVisaCardNumber()
        {
            List<string> visaList = new List<string>()
                                    { "4539", "4556", "4916", "4532", "4929",
                                      "40240071", "4485", "4716", "4" };
            Assert.IsTrue(visaList.Any(s => (CardNumberGenerator.GenerateVisaCardNumber()).StartsWith(s)));
            List<string> otherThanVisaList = new List<string>() { "22", "2222" };
            Assert.IsFalse(otherThanVisaList.Any(s => (CardNumberGenerator.GenerateVisaCardNumber()).StartsWith(s)));
        }

        [TestMethod]
        public void TestGenerateRewardsCardNumber()
        {
            List<string> rewardList = new List<string>() { "22", "2222" };
            Assert.IsTrue(rewardList.Any(s => (CardNumberGenerator.GenerateRewardsCardNumber()).StartsWith(s)));

            List<string> otherThanRewardList = new List<string>() { "4539", "4556", "4916" };
            Assert.IsFalse(otherThanRewardList.Any(s => (CardNumberGenerator.GenerateRewardsCardNumber()).StartsWith(s)));
        }

        [TestMethod]
        public void TestIsValidCreditCardNumber()
        {
            //Minimum length for card number should be validated.
            Assert.AreEqual(true, CardNumberGenerator.IsValidCreditCardNumber(""));
            Assert.AreEqual(true, CardNumberGenerator.IsValidCreditCardNumber("349220028381055"));
            Assert.AreEqual(true, CardNumberGenerator.IsValidCreditCardNumber("6011897748517631"));
            Assert.AreNotEqual(true, CardNumberGenerator.IsValidCreditCardNumber("6011897748517633"));
        }
    }
}
