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
    public class CheckDatabaseWaiting
    {
        DbHelper dbHelper = new DbHelper();
        readonly WaitingController waitingController = new WaitingController();

        [TestMethod]
        public void AddWaiting_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 3;
            string ticket = "А-1234-21";
            string login = "UliyaBay";

            int oldWaitingLength = dbHelper.context.waiting.Count();
            //Act
            if (waitingController.AddNewWaiting(login, book_id, ticket))
            {
                int updatedWaitingLength = dbHelper.context.waiting.Count();
                var selectString = dbHelper.context.waiting.OrderByDescending(t => t.waiting_id).FirstOrDefault().waiting_id;
                waitingController.DeleteEaitingBook(selectString);

                //Assert
                Assert.AreEqual(oldWaitingLength + 1, updatedWaitingLength);
            }

        }

        [TestMethod]
        public void AddWaiting_InCorrectData_FalseReturned()
        {
            //Arrange
            int book_id = 1;
            string ticket = "А-1234-214545";
            string login = "UliyaBay";

            int oldWaitingLength = dbHelper.context.waiting.Count();
            //Act
            bool check = waitingController.AddNewWaiting(login, book_id, ticket);

            //Assert
            Assert.IsFalse(check);

        }

        [TestMethod]
        public void AddWaiting_IsEmptyData_FalseReturned()
        {
            //Arrange
            int book_id = 0;
            string ticket = "";
            string login = "";

            int oldWaitingLength = dbHelper.context.waiting.Count();
            //Act
            bool check = waitingController.AddNewWaiting(login, book_id, ticket);

            //Assert
            Assert.IsFalse(check);

        }

        [TestMethod]
        public void DeleteWaiting_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 3;
            string ticket = "А-1234-21";
            string login = "UliyaBay";

            int oldWaitingLength = dbHelper.context.waiting.Count();
            //Act
            if (waitingController.AddNewWaiting(login, book_id, ticket))
            {

                var selectString = dbHelper.context.waiting.OrderByDescending(t => t.waiting_id).FirstOrDefault().waiting_id;
                waitingController.DeleteEaitingBook(selectString);
                int updatedWaitingLength = dbHelper.context.waiting.Count();
                //Assert
                Assert.AreEqual(oldWaitingLength, updatedWaitingLength);
            }
        }

        [TestMethod]
        public void DeleteWaiting_NullData_FalseReturned()
        {
            //Arrange
            int selectString = 0;

            //Act
            bool check = waitingController.DeleteEaitingBook(selectString);

            //Assert
            Assert.IsFalse(check);
        }
    }
}
