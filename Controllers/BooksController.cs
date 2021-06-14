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
using System.Windows.Controls;

namespace LibraryProject.Controllers
{
    public class BooksController
    {
        DbHelper dbHelper = new DbHelper();
        readonly TradingController tradingController = new TradingController();

        /// <summary>
        /// Формирование листа со всеми книгами
        /// </summary>
        /// <returns>
        /// Лист со всеми книгами
        /// </returns>
        public List<books> BooksInfoOutput()
        {
            return dbHelper.context.books.ToList();
        }

        /// <summary>
        /// Поиск совпадений полей authoe, isbn с вводимыми данными
        /// </summary>
        /// <param name="info" - строка, по которой ищутся совпадения>
        /// </param>
        /// <returns>
        /// Лист с совпадениями
        /// </returns>
        public List<books> BooksMatchUpInfoOutput(string info)
        {

            return dbHelper.context.books.Where(t => t.author.Contains(info) || t.isbn.Contains(info)).ToList();
        }

        /// <summary>
        /// Добавление новой книги
        /// </summary>
        /// <param name="bookAuthor" - имя автора></param>
        /// <param name="bookKnowledgeField" - идентификатор ббк></param>
        /// <param name="bookName" - название книги></param>
        /// <param name="bookISBN" - шифр isbn></param>
        /// <param name="bookPlace" - место издания книги></param>
        /// <param name="bookYear" - год издания книги></param>
        /// <param name="bookInterpreter" - идентификатор издательства></param>
        /// <param name="bookChamber" - идентификатор отсека></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddNewBook(string bookAuthor, int bookKnowledgeField, string bookName, string bookISBN, string bookPlace, int bookYear, int bookInterpreter, int bookChamber)
        {
            try
            {
                StringCheck check = new StringCheck();

                if ((!check.CheckName(bookAuthor) || bookAuthor == "") ||(!check.CheckBookName(bookName) || bookName == "") ||(!check.CheckBookIsbn(bookISBN) || bookISBN == "") || (!check.CheckBookYear(Convert.ToString(bookYear)) || bookYear > DateTime.Now.Year || bookYear < 1500) 
                    || (bookInterpreter == 0))
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
                        interpreter_id = bookInterpreter,
                        chamber_id = bookChamber,
                        trading_id = null
                    });

                    dbHelper.context.SaveChanges();
                    Settings.Default.bookId = dbHelper.context.books.OrderByDescending(t => t.book_id).FirstOrDefault().book_id;
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
        /// Удаление выбранной книги
        /// </summary>
        /// <param name="selectString" - выбранная пользователем строка DataGrid>
        /// </param>
        /// /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool DeleteBookInfo(books selectString)
        {
            try
            {
                if (selectString == null)
                {
                    return false;
                }
                else
                {
                    var selectTrading = from search in dbHelper.context.books
                                        where search.book_id == selectString.book_id
                                        select search;

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
            }    
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Формирование листа книг, которые взял пользователь
        /// </summary>
        /// <returns>
        /// Лист с книгами, которые взял пользователь
        /// </returns>
        public List<books> GetTradingBooks()
        {
           return dbHelper.context.books.Where(t => t.trading.login == Settings.Default.login).ToList();
        }

        /// <summary>
        /// Формирование листа книг, которые доступные для пользователя.
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

        public bool UpdateBookInfo(string newAuthor, int newfFieldKnowledgeId, string newName, string newIsbn, string newPlace, int newYear, int newInterpretorId, int newChamberId, List<books> oldBook)
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
                        item.interpreter_id = newInterpretorId;
                        item.chamber_id = newChamberId;
                        item.trading_id = null;
                        item.quantity_id = null;
                    }

                    dbHelper.context.SaveChanges();
                    if(dbHelper.context.SaveChanges() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
                
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool UpdateBookQuantity(int newQuantity)
        {
            try
            {
                    foreach (var item in dbHelper.context.books.OrderByDescending(t => t.book_id).ToList().Take(1))
                    {
                        item.quantity_id = newQuantity;
                    }

                    dbHelper.context.SaveChanges();
                    if (dbHelper.context.SaveChanges() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public int SelectedIndexBookComboBox(trading tradingInfoDataContext, ComboBox BookComboBox)
        {
            try
            {
                var comboBoxItem = BookComboBox.Items.OfType<books>().FirstOrDefault(x => x.book_id == tradingInfoDataContext.book_id);
                int index = BookComboBox.SelectedIndex = BookComboBox.Items.IndexOf(comboBoxItem);
                return index;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
