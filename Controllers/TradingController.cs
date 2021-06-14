using LibraryProject.Models;
using LibraryProject.Properties;
using StringCheckLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
    /// <summary>
    /// Класс для работы с данными таблицы "Trading"
    /// </summary>
    public class TradingController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        readonly DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Формирование листа со всеми выдачами
        /// </summary>
        /// <returns>
        /// Лист со всеми выдачами
        /// </returns>
        public List<trading> GetTradingInfo()
        {
            return dbHelper.context.trading.ToList();
        }

        /// <summary>
        /// Добавление новой выдачи
        /// </summary>
        /// <param name="bookId" - Идентификатор книги></param>
        /// <param name="ticket" - Читательский билет></param>
        /// <param name="deliveryDate" - Дата выдачи></param>
        /// <param name="receptionDate" - Максимальная дата возврата></param>
        /// <param name="userLogin" - Логин пользователя, который взял книгу></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddNewTrading(int bookId, string ticket, DateTime deliveryDate, DateTime receptionDate, string userLogin)
        {
            try
            {
                StringCheck check = new StringCheck();
                if (bookId != 0 && check.CheckTradingTicket(ticket) && check.CheckDate(Convert.ToString(deliveryDate.ToString("yyyy.MM.dd"))) && check.CheckDate(Convert.ToString(receptionDate.ToString("yyyy.MM.dd"))) && check.CheckLogin(userLogin))
                {
                    dbHelper.context.trading.Add(new trading
                    {
                        book_id = bookId,
                        ticket = ticket,
                        delivery = deliveryDate,
                        reception = receptionDate,
                        login = userLogin
                    });

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
        /// Поиск нужного билета
        /// </summary>
        /// <param name="tradingId" - Идентификатор выбранной книги></param>
        /// <returns>
        /// Искомый билет
        /// </returns>
        public string GetNeededTicket(int tradingId)
        {
            try
            {
                return dbHelper.context.trading.Where(t => t.trading_id == tradingId).First().ticket;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Поиск нужного идентификатора выдачи
        /// </summary>
        /// <param name="selectBook" - идентификатор выбранной книги></param>
        /// <returns>
        /// Искомый идентификатор выдачи
        /// </returns>
        public int GetNeededTradingId(int selectBook)
        {
            return dbHelper.context.trading.Where(t => t.book_id == selectBook).First().trading_id;
        }

        /// <summary>
        /// Формирвоание листа со всеми идентификаторами книг выдач
        /// </summary>
        /// <returns>
        /// Лист со всеми идентификаторами книг выдачи
        /// </returns>
        public List<int> GetBooksId()
        {
            List<int> tradingBooksId = new List<int>();

            foreach (var item in dbHelper.context.trading.ToList())
            {
                tradingBooksId.Add(item.book_id);
            }

            if(tradingBooksId != null)
            {
                return tradingBooksId;
            }
            else{
                return null;
            }
        }

        /// <summary>
        /// Формирование листа с нужной выдачой
        /// </summary>
        /// <param name="selectedBook" - Идентификатор книги></param>
        /// <returns>
        /// Лист с нужной выдачой
        /// </returns>
        public List<trading> GetTradingString(int selectedBook)
        {
            return dbHelper.context.trading.Where(t => t.trading_id == selectedBook).ToList();
        }

        /// <summary>
        /// Обновление выдачи
        /// </summary>
        /// <param name="newBook_id" - новый идентификатор книги></param>
        /// <param name="newTicket" - новый читательский билет></param>
        /// <param name="newDelivery" - новая дата выдачи></param>
        /// <param name="newReception" - новая максимальная дата возврата></param>
        /// <param name="oldBook" - лист со старыми данными></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool UpdateTradingInfo(int newBook_id, string newTicket, DateTime newDelivery, DateTime newReception, List<trading> oldBook)
        {
            try
            {
                StringCheck check = new StringCheck();
                if (newBook_id == 0 || !check.CheckTradingTicket(newTicket) || !check.CheckDate(Convert.ToString(newDelivery.ToString("yyyy.MM.dd"))) || !check.CheckDate(Convert.ToString(newReception.ToString("yyyy.MM.dd"))))
                {
                    return false;
                }

                else
                {
                    foreach (var item in oldBook)
                    {
                        item.book_id = newBook_id;
                        item.ticket = newTicket;
                        item.delivery = newDelivery;
                        item.reception = newReception;
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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Удаление выбранной выдачи
        /// </summary>
        /// <param name="selectString" - Идентификатор выбранной выдачи></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool RemoveTrading(int selectString)
        {
            try
            {
                if(selectString == 0)
                {
                    return false;
                }
                else
                {
                    var selectTrading = from search in dbHelper.context.trading
                                        where search.trading_id == selectString
                                        select search;

                    if (selectTrading != null)
                    {
                        foreach (var item in selectTrading)
                        {
                            dbHelper.context.trading.Remove(item);
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
    }
}
