using CheckBbkLib;
using LibraryProject.Controllers;
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
        FieldsController fieldsController = new FieldsController();
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

    }
}
