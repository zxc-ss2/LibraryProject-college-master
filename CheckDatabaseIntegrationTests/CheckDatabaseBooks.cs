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
            string author = "Андрей";
            int field_knowledge_id = 23;
            string name = "Сергеевич";
            string isbn = "1234-5634-34";
            string place = "Куйбышевf";
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
            string author = "Андрей";
            int field_knowledge_id = 234534;
            string name = "Сергеевич";
            string isbn = "1234-5634-34";
            string place = "Куйбышевf";
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
            string author = "Андрей";
            int field_knowledge_id = 34;
            string name = "Сергеевич";
            string isbn = "1234-5634-34";
            string place = "Куйбышевf";
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
            string author = "Андрей";
            int field_knowledge_id = 23;
            string name = "Сергеевич";
            string isbn = "1234-5634-34";
            string place = "Куйбышевf";
            int year = 1755;
            int interpreter_id = 2;
            int chamber_id = 2;
            string newName = "Аываы";

            string expectedName = "";
            //Act
            List<books> updatingBook = new List<books>();
            updatingBook = booksController.GetBookWithId(1);
            if (booksController.AddNewBook(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id))
            {

                if (booksController.UpdateBookInfo(author, field_knowledge_id, name, isbn, place, year, interpreter_id, chamber_id, updatingBook))
                {
                    dbHelper = new DbHelper();
                    expectedName = dbHelper.context.books.Where(t => t.name == newName).FirstOrDefault().name;

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
        public void DeleteBook_CorrectData_TrueReturned()
        {
            //Arrange
            string author = "Андрей";
            int field_knowledge_id = 23;
            string name = "Сергеевич";
            string isbn = "1234-5634-34";
            string place = "Куйбышевf";
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
        public void DeleteBook_CorrectData_FalseReturned()
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
