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

    }
}
