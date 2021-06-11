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
    public class CheckDatabaseFormular
    {
        readonly DbHelper dbHelper = new DbHelper();
        readonly FormularController formularController = new FormularController();
        [TestMethod]
        public void AddFormular_CorrectData_TrueReturned()
        {
            //Arrange
            string ticket = "А-1234-21";
            string book_delivery = "2002.01.23";
            string book_reception = "2002.02.23";
            string book_return = "2002.03.01";
            int book_id = 3;

            int oldFormularLength = dbHelper.context.formular.Count();
            int updatedFormularLength = 0;
            //Act
            if (formularController.AddFormularInfo(ticket, Convert.ToDateTime(book_delivery), Convert.ToDateTime(book_reception), book_id))
            {
                updatedFormularLength = dbHelper.context.formular.Count();
                var selectString = dbHelper.context.formular.AsNoTracking().OrderByDescending(t => t.formular_id).Take(1);

                var selectTrading = from search in dbHelper.context.formular
                                    where search.book_id == 2
                                    select search;

                if (selectTrading != null)
                {
                    foreach (var item in selectTrading)
                    {
                        dbHelper.context.formular.Remove(item);
                    }
                    dbHelper.context.SaveChanges();
                }


                //Assert
                Assert.AreEqual(oldFormularLength + 1, updatedFormularLength);
            }
        }

        [TestMethod]
        public void AddFormular_InCorrectData_FalseReturned()
        {
            //Arrange
            string ticket = "А-1234-12";
            string book_delivery = "2002.01.23";
            string book_reception = "2002.02.23";
            string book_return = "2002.03.01";
            int book_id = 0;

            //Act
            bool check = formularController.AddFormularInfo(ticket, Convert.ToDateTime(book_delivery), Convert.ToDateTime(book_reception), book_id);
            //Assert
            Assert.IsFalse(check);
            
        }

        [TestMethod]
        public void AddFormular_IsEmptytData_FalseReturned()
        {
            //Arrange
            string ticket = "";
            string book_delivery = "2007.05.26";
            string book_reception = "2007.06.26";
            string book_return = "";
            int book_id = 0;

            //Act
            bool check = formularController.AddFormularInfo(ticket, Convert.ToDateTime(book_delivery), Convert.ToDateTime(book_reception), book_id);
            //Assert
            Assert.IsFalse(check);

        }

        [TestMethod]
        public void EditFormular_CorrectData_TrueReturned()
        {
            //Arrange
            string ticket = "А-1234-12";
            string book_delivery = "2002.01.23";
            string book_reception = "2002.02.23";
            string book_return = "2002.03.01";
            int book_id = 3;

            //Act
            List<formular> updatingFormulart = new List<formular>();

            if (formularController.AddFormularInfo(ticket, Convert.ToDateTime(book_delivery), Convert.ToDateTime(book_reception), book_id))
            {
                int updatedFormularLength = dbHelper.context.clients.Count();
                if (formularController.AddBookReturnDate(Convert.ToDateTime(book_return), ticket))
                {
                    var expectedString = dbHelper.context.formular.Where(t => t.ticket == ticket).FirstOrDefault().book_return;

                    var selectString = dbHelper.context.formular.AsNoTracking().OrderByDescending(t => t.formular_id).Take(1);

                    var selectTrading = from search in dbHelper.context.formular
                                        where search.book_id == 2
                                        select search;

                    if (selectTrading != null)
                    {
                        foreach (var item in selectTrading)
                        {
                            dbHelper.context.formular.Remove(item);
                        }
                        dbHelper.context.SaveChanges();
                    }
                    //Assert
                    Assert.AreEqual(Convert.ToDateTime(book_return), Convert.ToDateTime(expectedString));
                }
            }
        }

        [TestMethod]
        public void EditFormular_InCorrectData_Returned()
        {
            //Arrange
            string ticket = "А-1234-12";
            string book_delivery = "2002.01.23";
            string book_reception = "2002.02.23";
            string book_return = null;
            int book_id = 3;

            //Act
            List<formular> updatingFormulart = new List<formular>();

            if (formularController.AddFormularInfo(ticket, Convert.ToDateTime(book_delivery), Convert.ToDateTime(book_reception), book_id))
            {
                int updatedFormularLength = dbHelper.context.clients.Count();
                if (formularController.AddBookReturnDate(Convert.ToDateTime(book_return), ticket))
                {
                    var expectedString = dbHelper.context.formular.Where(t => t.ticket == ticket).FirstOrDefault().book_return;

                    var selectString = dbHelper.context.formular.AsNoTracking().OrderByDescending(t => t.formular_id).Take(1);

                    var selectTrading = from search in dbHelper.context.formular
                                        where search.book_id == 2
                                        select search;

                    if (selectTrading != null)
                    {
                        foreach (var item in selectTrading)
                        {
                            dbHelper.context.formular.Remove(item);
                        }
                        dbHelper.context.SaveChanges();
                    }
                    //Assert
                    Assert.AreEqual(Convert.ToDateTime(book_return), Convert.ToDateTime(expectedString));
                }
            }
        }
    }
}
