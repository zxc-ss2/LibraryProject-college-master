using LibraryProject.Models;
using LibraryProject.Properties;
using StringCheckLib;
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
        readonly DbHelper dbHelper = new DbHelper();
        readonly TradingController tradingController = new TradingController();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<books> BooksInfoOutput()
        {
            return dbHelper.context.books.ToList();
        }

        /// <summary>
        /// Проверка на совпадение вводимых данных с данными в бд
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public List<books> BooksMatchUpInfoOutput(string author)
        {
            return dbHelper.context.books.Where(t => t.author.Contains(author) || t.isbn.Contains(author)).ToList();
        }

        public bool AddNewBook(string bookAuthor, int bookKnowledgeField, string bookName, string bookISBN, string bookPlace, int bookYear, int bookInterpreter, int bookChamber)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (!check.CheckName(bookAuthor)|| !check.CheckBookName(bookName) || !check.CheckBookIsbn(bookISBN) || !check.CheckBookYear(Convert.ToString(bookYear)) 
                    || bookInterpreter == 0)
                {
                    return false;
                }
                else
                {
                    dbHelper.context.books.Add(new books
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Удаляет всю информацию о выбранной книге
        /// </summary>
        /// <param name="selectString" - выбранная пользователем строка DataGrid>
        /// </param>
        public bool DeleteBookInfo(books selectString)
        {
            try
            {
                var selectTrading = from zxc in dbHelper.context.books
                                    where zxc.book_id == selectString.book_id
                                    select zxc;

                if (selectTrading != null)
                {
                    foreach (var item in selectTrading)
                    {
                        dbHelper.context.books.Remove(item);
                    }
                    dbHelper.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Возвращает список книг, которые взял пользователь
        /// </summary>
        /// <returns></returns>
        public List<books> GetTradingBooks()
        {
           return dbHelper.context.books.Where(t => t.trading.login == Settings.Default.login).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public List<books> GetAvailableBooks(string userLogin)
        {
            List<books> AvailbleBooksList = new List<books>();
            try
            {
                if (tradingController.GetBooksId().Count == 0)
                {
                    AvailbleBooksList = dbHelper.context.books.Where(t => t.quantity.library_quantity > 0).ToList();
                }
                else
                {
                    foreach (var i in BooksInfoOutput())
                    {
                        foreach (var item in tradingController.GetBooksId())
                        {
                            AvailbleBooksList = dbHelper.context.books.Where(t => t.quantity.library_quantity > 0 && t.book_id != item && t.trading.login != userLogin).ToList();
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return AvailbleBooksList;

        }

        public bool AssignIdTradingToBook(int selectBook, int newTradingId)
        {
            try
            {
                foreach (var item in dbHelper.context.books.Where(t => t.book_id == selectBook).ToList())
                {
                    item.trading_id = newTradingId;
                }

                dbHelper.context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }        

        }

        public bool RemoveIdTradingFromBook(int selectBook)
        {
            try
            {
                foreach (var item in dbHelper.context.books.Where(t => t.book_id == selectBook).ToList())
                {
                    item.trading_id = null;
                }

                dbHelper.context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        public List<books> GetBookWithId(int selectBookId)
        {
            return dbHelper.context.books.Where(t => t.book_id == selectBookId).ToList();
        }

        public bool UpdateBookInfo(string newAuthor, int newfFieldKnowledgeId, string newName, string newIsbn, string newPlace, int newYear, int newQuantityId, int newStorageId, int newInterpretorId, int newChamberId, List<books> oldBook)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (!check.CheckName(newAuthor) || !check.CheckBookName(newName) || !check.CheckBookIsbn(newIsbn) || !check.CheckBookYear(Convert.ToString(newYear))
                    || newInterpretorId == 0)
                {
                    return false;
                }
                else
                {
                    foreach (var item in oldBook)
                    {
                        item.author = newAuthor;
                        item.field_knowledge_id = newfFieldKnowledgeId;
                        item.name = newName;
                        item.isbn = newIsbn;
                        item.place = newPlace;
                        item.year = newYear;
                        item.quantity_id = newQuantityId;
                        item.storage_id = newStorageId;
                        item.interpreter_id = newInterpretorId;
                        item.chamber_id = newChamberId;
                        item.trading_id = null;
                    }

                    dbHelper.context.SaveChanges();
                    return true;
                }
            }
                
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
