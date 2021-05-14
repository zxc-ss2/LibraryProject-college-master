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
        public void CheckBBK_isCorrect_TrueReturned()
        {
            //Arrange
            string stringBBK = "12.645";
            //Act 
            StringCheck isBBK = new StringCheck();
            bool correctBBK = isBBK.CheckBBK(stringBBK);
            //Assert
            Assert.IsTrue(correctBBK);
        }

        [TestMethod]
        public void CheckBBK_isEmpty_FalseReturned()
        {
            //Arrange
            string stringBBK = "";
            //Act 
            StringCheck isBBK = new StringCheck();
            bool correctBBK = isBBK.CheckBBK(stringBBK);
            //Assert
            Assert.IsFalse(correctBBK);
        }

        [TestMethod]
        public void CheckBBK_isEngLetters_FalseReturned()
        {
            //Arrange
            string stringBBK = "sdfsdgfdgdf";
            //Act 
            StringCheck isBBK = new StringCheck();
            bool correctBBK = isBBK.CheckBBK(stringBBK);
            //Assert
            Assert.IsFalse(correctBBK);
        }

        [TestMethod]
        public void CheckBBK_isRusLetters_FalseReturned()
        {
            //Arrange
            string stringBBK = "авпвапвапва";
            //Act 
            StringCheck isBBK = new StringCheck();
            bool correctBBK = isBBK.CheckBBK(stringBBK);
            //Assert
            Assert.IsFalse(correctBBK);
        }
    }
}
