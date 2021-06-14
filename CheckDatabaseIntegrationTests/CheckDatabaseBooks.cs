using System;
using System.Collections.Generic;
using System.Linq;
using LibraryProject.Controllers;
using LibraryProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckDatabaseIntegrationTests
{
    [TestClass]
    public class CheckDatabaseBooks
    {
        DbHelper dbHelper = new DbHelper();
        readonly BooksController booksController = new BooksController();
        [TestMethod]
        public void AddBook_CorrectData_TrueReturned()
        {
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 523;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;

            int updatedBooksLength = 0;
            int oldBooksLength = dbHelper.context.books.Count();
            //Act
            if (booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id))
            {
                updatedBooksLength = dbHelper.context.books.Count();
                var selectString = dbHelper.context.books.AsNoTracking().OrderByDescending(t => t.book_id).Take(1);

                foreach (var item in selectString)
                {
                    booksController.DeleteBookInfo(item);
                }


                //Assert
                Assert.AreEqual(oldBooksLength + 1, updatedBooksLength);
            }

        }

        [TestMethod]
        public void AddBook_IncorrectData_FalseReturned()
        {
            //Arrange
            string author = "";
            int field_knowledge_id = 0;
            string name = "";
            string isbn = "";
            string place = "";
            int year = 0;
            int interpreter_id = 0;
            int chamber_id = 0;

            int updatedBooksLength = 0;
            int oldBooksLength = dbHelper.context.books.Count();
            //Act
            bool check = booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id);

            //Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void AddBook_IncorrectDataFieldKnowledgeIdDoesntExists_FalseReturned()
        {
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 234534;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;

            //Act
            bool check = booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id);

            //Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void AddBook_IncorrectDataInterpreteriIdDoesntExists_FalseReturned()
        {
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 34;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2342;
            int chamber_id = 2;

            //Act
            bool check = booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id);

            //Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void EditBook_CorrectData_TrueReturned()
        {
            //Arrange
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 34;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;
            string newName = "Аываы";

            //Act
            List<books> updatingClient = new List<books>();

            if (booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id))
            {
                int tradingId = dbHelper.context.books.OrderByDescending(t => t.book_id).FirstOrDefault().book_id;
                updatingClient = booksController.GetBookId(tradingId);

                if (booksController.UpdateBookInfo(author, field_knowledge_id, newName, isbn, place, year, interpreter_id, chamber_id, updatingClient))
                {
                    dbHelper = new DbHelper();
                    string expectedName = dbHelper.context.books.Where(t => t.name == newName).FirstOrDefault().name;

                    var selectString = dbHelper.context.books.AsNoTracking().OrderByDescending(t => t.book_id).Take(1);
                    foreach (var item in selectString)
                    {
                        booksController.DeleteBookInfo(item);
                    }
                    //Assert
                    Assert.AreEqual(expectedName, newName);
                }
            }
        }

        [TestMethod]
        public void EditBook_IsEmptytData_FalseReturned()
        {
            //Arrange
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 34;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;
            string newName = "";

            //Act
            List<books> updatingClient = new List<books>();

            if (booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id))
            {
                var selectTrading = dbHelper.context.books.OrderByDescending(t => t.book_id).ToList().Take(1);
                foreach (var item in selectTrading)
                {
                    updatingClient.Add(item);
                };

                bool check = booksController.UpdateBookInfo(author, field_knowledge_id, newName, isbn, place, year, interpreter_id, chamber_id, updatingClient);
                {
                    //Assert
                    Assert.IsFalse(check);
                }
            }
        }

        [TestMethod]
        public void EditBook_IncorrecttData_FalseReturned()
        {
            //Arrange
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 34;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;
            string newName = "#$%#$%^";

            //Act
            List<books> updatingClient = new List<books>();

            if (booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id))
            {
                var selectTrading = dbHelper.context.books.OrderByDescending(t => t.book_id).ToList().Take(1);
                foreach (var item in selectTrading)
                {
                    updatingClient.Add(item);
                };

                bool check = booksController.UpdateBookInfo(author, field_knowledge_id, newName, isbn, place, year, interpreter_id, chamber_id, updatingClient);
                {
                    //Assert
                    Assert.IsFalse(check);
                }
            }
        }

        [TestMethod]
        public void DeleteBook_CorrectData_TrueReturned()
        {
            //Arrange
            string author = "Джорджи Кроули";
            int field_knowledge_id = 34;
            string name = "Корги по имени Генри";
            string isbn = "978-5-386-12347-5";
            string place = "Москва";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;

            int oldClientsLength = dbHelper.context.clients.Count();
            int updatedClientsLength = 0;
            //Act
            List<clients> updatingClient = new List<clients>();
            if (booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id))
            {

                var selectString = dbHelper.context.books.AsNoTracking().OrderByDescending(t => t.book_id).Take(1);
                foreach (var item in selectString)
                {
                    booksController.DeleteBookInfo(item);
                }
                updatedClientsLength = dbHelper.context.clients.Count();


                //Assert
                Assert.AreEqual(oldClientsLength, updatedClientsLength);
            }

        }

        [TestMethod]
        public void DeleteBook_InCorrectData_FalseReturned()
        {
            //Arrange
            books selectString = null;
            int oldClientsLength = dbHelper.context.clients.Count();

            //Act
            bool check = booksController.DeleteBookInfo(selectString);

            //Assert
            Assert.IsFalse(check);
        }

    }
}
