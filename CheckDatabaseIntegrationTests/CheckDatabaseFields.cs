using CheckBbkLib;
using LibraryProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckDatabaseIntegrationTests
{
    [TestClass]
    public class CheckDatabaseFields
    {
        readonly DbHelper dbHelper = new DbHelper();
        CheckBbk check = new CheckBbk();


        [TestMethod]
        public void CheckBbk_CorrectData_TrueReturned()
        {
            //Arrange
            string bbk = "26.343.1";

            //Act
            bool trigger = check.CheckBbkString(bbk);

            //Assert
            Assert.IsTrue(trigger);
        }

        [TestMethod]
        public void CheckBbk_EmptyData_FalseReturned()
        {
            //Arrange
            string bbk = "";

            //Act
            bool trigger = check.CheckBbkString(bbk);

            //Assert
            Assert.IsFalse(trigger);
        }

        [TestMethod]
        public void CheckBbk_IncorrectData_FalseReturned()
        {
            //Arrange
            string bbk = "234.234.12";

            //Act
            bool trigger = check.CheckBbkString(bbk);

            //Assert
            Assert.IsFalse(trigger);
        }
    }
}
