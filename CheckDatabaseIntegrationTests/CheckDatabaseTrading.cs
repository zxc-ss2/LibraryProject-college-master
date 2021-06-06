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
    public class CheckDatabaseTrading
    {
        DbHelper dbHelper = new DbHelper();
        readonly TradingController tradingController = new TradingController();
        readonly BooksController booksController = new BooksController();
        [TestMethod]
        public void AddTrading_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 1;
            string ticket = "А-1234-21";
            string delivery = "2002.03.01";
            string reception = "2002.04.01";
            string login = "UliyaBay";

            int oldClientsLength = dbHelper.context.trading.Count();
            //Act
            tradingController.AddNewTrading(book_id, ticket, Convert.ToDateTime(delivery), Convert.ToDateTime(reception), login);
            int updatedClientsLength = dbHelper.context.trading.Count();
            var selectString = dbHelper.context.trading.OrderByDescending(t => t.trading_id).FirstOrDefault().trading_id;
            tradingController.RemoveTrading(selectString);
            //Assert
            Assert.AreEqual(oldClientsLength + 1, updatedClientsLength);
        }

        [TestMethod]
        public void EditTrading_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 1;
            string ticket = "А-1234-21";
            string delivery = "2002.03.01";
            string reception = "2002.04.01";
            string login = "UliyaBay";
            string newTicket = "А-2134-21";
            dbHelper = new DbHelper();
            //Act
            List<trading> updatingTrading = new List<trading>();
            if (tradingController.AddNewTrading(book_id, ticket, Convert.ToDateTime(delivery), Convert.ToDateTime(reception), login))
            {
                var selectTrading = dbHelper.context.trading.OrderByDescending(t => t.trading_id).ToList().Take(1);
                foreach (var item in selectTrading)
                {
                    updatingTrading.Add(item);
                }

                if (tradingController.UpdateTradingInfo(Convert.ToInt32(book_id), newTicket, Convert.ToDateTime(delivery), Convert.ToDateTime(reception), updatingTrading))
                {
                    dbHelper = new DbHelper();
                    string expectedTicket = dbHelper.context.trading.Where(t => t.ticket == newTicket).First().ticket;

                    dbHelper = new DbHelper();
                    var selectString = dbHelper.context.trading.OrderByDescending(t => t.trading_id).FirstOrDefault().trading_id;
                    tradingController.RemoveTrading(selectString);

                    //Assert
                    Assert.AreEqual(newTicket, expectedTicket);

                }
            }
        }


        [TestMethod]
        public void DeleteTrading_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 1;
            string ticket = "А-1234-21";
            string delivery = "2002.03.01";
            string reception = "2002.04.01";
            string login = "UliyaBay";

            int oldTradingLength = dbHelper.context.trading.Count();
            int updatedTradingLength = 0;
            //Act
            if (tradingController.AddNewTrading(book_id, ticket, Convert.ToDateTime(delivery), Convert.ToDateTime(reception), login))
            {

                var selectString = dbHelper.context.trading.OrderByDescending(t => t.trading_id).FirstOrDefault().trading_id;
                tradingController.RemoveTrading(selectString);

                updatedTradingLength = dbHelper.context.trading.Count();

                //Assert
                Assert.AreEqual(oldTradingLength, updatedTradingLength);
            }
        }

    }
}
