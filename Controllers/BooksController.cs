using LibraryProject.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
    public class BooksController
    {
        readonly Models.DbHelper dbHelper = new Models.DbHelper();
        readonly TradingController tradingController = new TradingController();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Models.books> BooksInfoOutput()
        {
            return dbHelper.context.books.ToList();
        }

        /// <summary>
        /// Проверка на совпадение вводимых данных с данными в бд
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public List<Models.books> BooksMatchUpInfoOutput(string author)
        {
            return dbHelper.context.books.Where(t => t.author.Contains(author) || t.isbn.Contains(author)).ToList();
        }

        public bool AddNewBook(string bookAuthor, int bookKnowledgeField, string bookName, string bookISBN, string bookPlace, int bookYear, int bookInterpreter, int bookChamber)
        {
            dbHelper.context.books.Add(new Models.books
            {
                author = bookAuthor,
                field_knowledge_id = bookKnowledgeField,
                name = bookName,
                isbn = bookISBN,
                place = bookPlace,
                year = bookYear,
                quantity_id = null,
                storage_id = null,
                interpreter_id = bookInterpreter,
                chamber_id = bookChamber,
                trading_id = null
            });

            dbHelper.context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Удаляет всю информацию о выбранной книге
        /// </summary>
        /// <param name="selectString" - выбранная пользователем строка DataGrid>
        /// </param>
        public void DeleteBookInfo(Models.books selectString)
        {
            dbHelper.context.books.Remove(selectString);
            dbHelper.context.SaveChanges();
            MessageBox.Show("Удалена информация о" + selectString);
        }

        /// <summary>
        /// Возвращает список книг, которые взял пользователь
        /// </summary>
        /// <returns></returns>
        public List<Models.books> GetTradingBooks()
        {
           return dbHelper.context.books.Where(t => t.trading.login == Settings.Default.login).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public List<Models.books> GetAvailableBooks(string userLogin)
        {
            List<Models.books> AvailbleBooksList = new List<Models.books>();

            foreach (var item in tradingController.GetBooksId())
            {
                //dbHelper.context.books.Where(t => t.quantity.library_quantity > 0 && t.trading.book_id != selectBook && t.trading.login != userLogin).ToList();
                AvailbleBooksList = dbHelper.context.books.Where(t => t.book_id != item && t.trading.login != userLogin).ToList();
            }

            if (AvailbleBooksList == null)
            {
                return null;
            }
            else
            {
                return AvailbleBooksList;
            }
        }

        public bool AssignIdTradingToBook(int selectBook, int newTradingId, List<Models.books> selectBookList)
        {
            var bookTrading = dbHelper.context.books.Where(t => t.book_id == selectBook).First().trading_id;

            foreach (var item in selectBookList)
            {
                item.trading_id = newTradingId;
            }

            dbHelper.context.SaveChanges();
            return true;
        }
    }
}
