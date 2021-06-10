using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCheckLib;

namespace StringCheckLibTests
{
    [TestClass]
    public class StringCheckTests
    {
        [TestMethod]
        public void CheckName_isRus_TrueReturned()
        {
            //Arrange
            string stringName = "Алексей";
            //Act 
            StringCheck isName = new StringCheck();
            bool correctName = isName.CheckName(stringName);
            //Assert
            Assert.IsTrue(correctName);
        }

        [TestMethod]
        public void CheckName_isEmpty_FalseReturned()
        {
            //Arrange
            string stringName = "";
            //Act 
            StringCheck isName = new StringCheck();
            bool correctName = isName.CheckName(stringName);
            //Assert
            Assert.IsFalse(correctName);
        }

        [TestMethod]
        public void CheckName_isSpace_FalseReturned()
        {
            //Arrange
            string stringName = "Ал ексей";
            //Act 
            StringCheck isName = new StringCheck();
            bool correctName = isName.CheckName(stringName);
            //Assert
            Assert.IsFalse(correctName);
        }

        [TestMethod]
        public void CheckName_isEngLetters_FalseReturned()
        {
            //Arrange
            string stringName = "Alexey";
            //Act 
            StringCheck isName = new StringCheck();
            bool correctName = isName.CheckName(stringName);
            //Assert
            Assert.IsFalse(correctName);
        }

        [TestMethod]
        public void CheckAddress_isRusSpaceDelimiter_TrueReturned()
        {
            //Arrange
            string stringAddress = "Щорса 56";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsTrue(correctAddress);
        }

        [TestMethod]
        public void CheckAddress_isRusCommaDelimiter_TrueReturned()
        {
            //Arrange
            string stringAddress = "Щорса,56";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsTrue(correctAddress);
        }

        [TestMethod]
        public void CheckAddress_isRusDashDelimiter_TrueReturned()
        {
            //Arrange
            string stringAddress = "Щорса-56";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsTrue(correctAddress);
        }

        [TestMethod]
        public void CheckAddress_isEmmpty_FalseReturned()
        {
            //Arrange
            string stringAddress = "";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsFalse(correctAddress);
        }

        [TestMethod]
        public void CheckAddress_isRusWithOutNumbers_FalseReturned()
        {
            //Arrange
            string stringAddress = "Щорса";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsFalse(correctAddress);
        }

        [TestMethod]
        public void CheckAddress_isEng_FalseReturned()
        {
            //Arrange
            string stringAddress = "Shorsa 56";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsFalse(correctAddress);
        }

        [TestMethod]
        public void CheckAddress_isEngWithOutDilimiter_FalseReturned()
        {
            //Arrange
            string stringAddress = "Щорса56";
            //Act 
            StringCheck isAddress = new StringCheck();
            bool correctAddress = isAddress.CheckAddress(stringAddress);
            //Assert
            Assert.IsFalse(correctAddress);
        }

        [TestMethod]
        public void CheckPhone_isCorrectPhoneThatStartsWithNumber8_TrueReturned()
        {
            //Arrange
            string stringPhone = "89089150664";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsTrue(correctPhone);
        }

        [TestMethod]
        public void CheckPhone_isCorrectPhoneThatStartsWithNumberPlus7_TrueReturned()
        {
            //Arrange
            string stringPhone = "+79089150664";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsTrue(correctPhone);
        }


        [TestMethod]
        public void CheckPhone_isCorrectPhoneThatStartsWithNumber7_TrueReturned()
        {
            //Arrange
            string stringPhone = "79089150664";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsTrue(correctPhone);
        }

        [TestMethod]
        public void CheckPhone_isEmpty_FalseReturned()
        {
            //Arrange
            string stringPhone = "";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsFalse(correctPhone);
        }

        [TestMethod]
        public void CheckPhone_isTooMuchChars_FalseReturned()
        {
            //Arrange
            string stringPhone = "89089150662265";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsFalse(correctPhone);
        }

        [TestMethod]
        public void CheckPhone_isDataWithLetters_FalseReturned()
        {
            //Arrange
            string stringPhone = "89089g15п";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsFalse(correctPhone);
        }

        [TestMethod]
        public void CheckPhone_isNotEnoughChars_FalseReturned()
        {
            //Arrange
            string stringPhone = "8908963";
            //Act 
            StringCheck isPhone = new StringCheck();
            bool correctPhone = isPhone.CheckPhone(stringPhone);
            //Assert
            Assert.IsFalse(correctPhone);
        }

        [TestMethod]
        public void CheckLogin_isCorrect_TrueReturned()
        {
            //Arrange
            string stringLogin = "cursed_1234";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsTrue(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isUppercaseLogin_TrueReturned()
        {
            //Arrange
            string stringLogin = "CURSED_1234";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsTrue(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isEmpty_FalseReturned()
        {
            //Arrange
            string stringLogin = "";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsFalse(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isTwoInARowDash_FalseReturned()
        {
            //Arrange
            string stringLogin = "cursed--1234";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsFalse(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isTwoInARowLowDash_FalseReturned()
        {
            //Arrange
            string stringLogin = "cursed__1234";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsFalse(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isMoreThanTwoDash_FalseReturned()
        {
            //Arrange
            string stringLogin = "cursed-12-3-4";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsFalse(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isMoreThanTwoLowDash_FalseReturned()
        {
            //Arrange
            string stringLogin = "cursed_12_3_4";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsFalse(correctLogin);
        }

        [TestMethod]
        public void CheckLogin_isRus_FalseReturned()
        {
            //Arrange
            string stringLogin = "юзер123";
            //Act 
            StringCheck isLogin = new StringCheck();
            bool correctLogin = isLogin.CheckLogin(stringLogin);
            //Assert
            Assert.IsFalse(correctLogin);
        }

        [TestMethod]
        public void CheckPassword_isCorrect_TrueReturned()
        {
            //Arrange
            string stringPassword = "ThatStuffW1llKillYa!";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsTrue(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isNotEnoughChars_FalseReturned()
        {
            //Arrange
            string stringPassword = "zxcV1_";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isNotEnoughspecialChars_FalseReturned()
        {
            //Arrange
            string stringPassword = "zxcvQWER1234";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isNotEnoughNumbers_FalseReturned()
        {
            //Arrange
            string stringPassword = "zxcQWER_!gf";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isNotEnoughUppercaseLetters_FalseReturned()
        {
            //Arrange
            string stringPassword = "zxcvbb!_f32";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isNotEnoughLowcaseLetters_FalseReturned()
        {
            //Arrange
            string stringPassword = "FSFSD123_!fd";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isRusLetters_FalseReturned()
        {
            //Arrange
            string stringPassword = "йцукячс!_АВ";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckPassword_isEmpty_FalseReturned()
        {
            //Arrange
            string stringPassword = "";
            //Act 
            StringCheck isPassword = new StringCheck();
            bool correctPassword = isPassword.CheckPassword(stringPassword);
            //Assert
            Assert.IsFalse(correctPassword);
        }

        [TestMethod]
        public void CheckBookName_isCorrectRus_TrueReturned()
        {
            //Arrange
            string stringBookName = "Ведьмак";
            //Act 
            StringCheck isBookName = new StringCheck();
            bool correctBooks = isBookName.CheckBookName(stringBookName);
            //Assert
            Assert.IsTrue(correctBooks);
        }

        [TestMethod]
        public void CheckBookName_isCorrectEng_TrueReturned()
        {
            //Arrange
            string stringBookName = "The Witcher";
            //Act 
            StringCheck isBookName = new StringCheck();
            bool correctBooks = isBookName.CheckBookName(stringBookName);
            //Assert
            Assert.IsTrue(correctBooks);
        }

        [TestMethod]
        public void CheckBookName_isSpecialCharacters_FalseReturned()
        {
            //Arrange
            string stringBookName = "!!__&%^$";
            //Act 
            StringCheck isBookName = new StringCheck();
            bool correctBooks = isBookName.CheckBookName(stringBookName);
            //Assert
            Assert.IsFalse(correctBooks);
        }

        [TestMethod]
        public void CheckBookName_isEmpty_FalseReturned()
        {
            //Arrange
            string stringBookName = "";
            //Act 
            StringCheck isBookName = new StringCheck();
            bool correctBooks = isBookName.CheckBookName(stringBookName);
            //Assert
            Assert.IsFalse(correctBooks);
        }

        [TestMethod]
        public void CheckBookIsbn_isCorrect_TrueReturned()
        {
            //Arrange
            string stringBookIsbn = "123-4-33455-111-5";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsTrue(correctIsbn);
        }

        [TestMethod]
        public void CheckBookIsbn_isEmpty_FalseReturned()
        {
            //Arrange
            string stringBookIsbn = "";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsFalse(correctIsbn);
        }


        [TestMethod]
        public void CheckBookIsbn_isEngLetters_FalseReturned()
        {
            //Arrange
            string stringBookIsbn = "Test Data";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsFalse(correctIsbn);
        }

        [TestMethod]
        public void CheckBookIsbn_isRusLetters_FalseReturned()
        {
            //Arrange
            string stringBookIsbn = "Тестовый ввод";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsFalse(correctIsbn);
        }

        [TestMethod]
        public void CheckBookIsbn_isWithOutDilimiter_FalseReturned()
        {
            //Arrange
            string stringBookIsbn = "1234567890152";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsFalse(correctIsbn);
        }

        [TestMethod]
        public void CheckBookIsbn_isNotEnoughChars_FalseReturned()
        {
            //Arrange
            string stringBookIsbn = "536-2-5637-363-2";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsFalse(correctIsbn);
        }

        [TestMethod]
        public void CheckBookIsbn_isTooMuchChars_FalseReturned()
        {
            //Arrange
            string stringBookIsbn = "536-2-56237-3653-2";
            //Act 
            StringCheck isBookIsbn = new StringCheck();
            bool correctIsbn = isBookIsbn.CheckBookIsbn(stringBookIsbn);
            //Assert
            Assert.IsFalse(correctIsbn);
        }

        [TestMethod]
        public void CheckBookYear_isCorrect_TrueReturned()
        {
            //Arrange
            string stringBookYear = "1994";
            //Act 
            StringCheck isBookYear = new StringCheck();
            bool correctYear = isBookYear.CheckBookYear(stringBookYear);
            //Assert
            Assert.IsTrue(correctYear);
        }

        [TestMethod]
        public void CheckBookYear_isEmpty_FalseReturned()
        {
            //Arrange
            string stringBookYear = "";
            //Act 
            StringCheck isBookYear = new StringCheck();
            bool correctYear = isBookYear.CheckBookYear(stringBookYear);
            //Assert
            Assert.IsFalse(correctYear);
        }

        [TestMethod]
        public void CheckBookYear_isTooLowNumbers_FalseReturned()
        {
            //Arrange
            string stringBookYear = "222";
            //Act 
            StringCheck isBookYear = new StringCheck();
            bool correctYear = isBookYear.CheckBookYear(stringBookYear);
            //Assert
            Assert.IsFalse(correctYear);
        }


        [TestMethod]
        public void CheckBookYear_isTooMuchNumbers_FalseReturned()
        {
            //Arrange
            string stringBookYear = "222222222";
            //Act 
            StringCheck isBookYear = new StringCheck();
            bool correctYear = isBookYear.CheckBookYear(stringBookYear);
            //Assert
            Assert.IsFalse(correctYear);
        }

        [TestMethod]
        public void CheckBookYear_isRusLetters_FalseReturned()
        {
            //Arrange
            string stringBookYear = "ываываыва";
            //Act 
            StringCheck isBookYear = new StringCheck();
            bool correctYear = isBookYear.CheckBookYear(stringBookYear);
            //Assert
            Assert.IsFalse(correctYear);
        }

        [TestMethod]
        public void CheckBookYear_isEngLetters_FalseReturned()
        {
            //Arrange
            string stringBookYear = "dfgdfgdfg";
            //Act 
            StringCheck isBookYear = new StringCheck();
            bool correctYear = isBookYear.CheckBookYear(stringBookYear);
            //Assert
            Assert.IsFalse(correctYear);
        }

        [TestMethod]
        public void CheckTicket_isCorrect_TrueReturned()
        {
            //Arrange
            string stringTicket = "А-1243-23";
            //Act 
            StringCheck isBookTicket = new StringCheck();
            bool correctTicket = isBookTicket.CheckTradingTicket(stringTicket);
            //Assert
            Assert.IsTrue(correctTicket);
        }

        [TestMethod]
        public void CheckTicket_isEmpty_TrueReturned()
        {
            //Arrange
            string stringTicket = "";
            //Act 
            StringCheck isBookTicket = new StringCheck();
            bool correctTicket = isBookTicket.CheckTradingTicket(stringTicket);
            //Assert
            Assert.IsFalse(correctTicket);
        }

        [TestMethod]
        public void CheckTicket_isOnlyRusLetters_TrueReturned()
        {
            //Arrange
            string stringTicket = "выааываыва";
            //Act 
            StringCheck isBookTicket = new StringCheck();
            bool correctTicket = isBookTicket.CheckTradingTicket(stringTicket);
            //Assert
            Assert.IsFalse(correctTicket);
        }

        [TestMethod]
        public void CheckTicket_isOnlyEngLetters_TrueReturned()
        {
            //Arrange
            string stringTicket = "dfgdfgg";
            //Act 
            StringCheck isBookTicket = new StringCheck();
            bool correctTicket = isBookTicket.CheckTradingTicket(stringTicket);
            //Assert
            Assert.IsFalse(correctTicket);
        }

        [TestMethod]
        public void CheckTicket_isOnlyNumbers_TrueReturned()
        {
            //Arrange
            string stringTicket = "1231231";
            //Act 
            StringCheck isBookTicket = new StringCheck();
            bool correctTicket = isBookTicket.CheckTradingTicket(stringTicket);
            //Assert
            Assert.IsFalse(correctTicket);
        }

        [TestMethod]
        public void CheckDate_isCorrect_TrueReturned()
        {
            //Arrange
            string stringDate = "2020.05.14";
            //Act 
            StringCheck isDate = new StringCheck();
            bool correctDate = isDate.CheckDate(stringDate);
            //Assert
            Assert.IsTrue(correctDate);
        }

        [TestMethod]
        public void CheckDate_isEmpty_FalseReturned()
        {
            //Arrange
            string stringDate = "";
            //Act 
            StringCheck isDate = new StringCheck();
            bool correctDate = isDate.CheckDate(stringDate);
            //Assert
            Assert.IsFalse(correctDate);
        }

        [TestMethod]
        public void CheckDate_isRusLetters_FalseReturned()
        {
            //Arrange
            string stringDate = "вапвапвапва";
            //Act 
            StringCheck isDate = new StringCheck();
            bool correctDate = isDate.CheckDate(stringDate);
            //Assert
            Assert.IsFalse(correctDate);
        }

        [TestMethod]
        public void CheckDate_isEngLetters_FalseReturned()
        {
            //Arrange
            string stringDate = "fdgdfgdfgdf";
            //Act 
            StringCheck isDate = new StringCheck();
            bool correctDate = isDate.CheckDate(stringDate);
            //Assert
            Assert.IsFalse(correctDate);
        }

        [TestMethod]
        public void CheckDate_isDateWithOutCharacters_FalseReturned()
        {
            //Arrange
            string stringDate = "20020514";
            //Act 
            StringCheck isDate = new StringCheck();
            bool correctDate = isDate.CheckDate(stringDate);
            //Assert
            Assert.IsFalse(correctDate);
        }

        [TestMethod]
        public void CheckEmail_isCorrect_TrueReturned()
        {
            //Arrange
            string stringEmail = "skochkov@gmail.com";
            //Act 
            StringCheck isEmail = new StringCheck();
            bool correctEmail = isEmail.CheckEmail(stringEmail);
            //Assert
            Assert.IsTrue(correctEmail);
        }

        [TestMethod]
        public void CheckEmail_isEmpty_FalseReturned()
        {
            //Arrange
            string stringEmail = "";
            //Act 
            StringCheck isEmail = new StringCheck();
            bool correctEmail = isEmail.CheckEmail(stringEmail);
            //Assert
            Assert.IsFalse(correctEmail);
        }

        [TestMethod]
        public void CheckEmail_isRusLetters_FalseReturned()
        {
            //Arrange
            string stringEmail = "выаыва.попр@аыва";
            //Act 
            StringCheck isEmail = new StringCheck();
            bool correctEmail = isEmail.CheckEmail(stringEmail);
            //Assert
            Assert.IsFalse(correctEmail);
        }

        [TestMethod]
        public void CheckEmail_isOnlyNumbers_FalseReturned()
        {
            //Arrange
            string stringEmail = "1111@2131232";
            //Act 
            StringCheck isEmail = new StringCheck();
            bool correctEmail = isEmail.CheckEmail(stringEmail);
            //Assert
            Assert.IsFalse(correctEmail);
        }
    }
}
