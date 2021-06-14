using LibraryProject.Models;
using StringCheckLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
    /// <summary>
    /// Класс для работы с данными таблицы "Waiting"
    /// </summary>
    public class WaitingController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        readonly DbHelper dbHelper = new DbHelper();

        /// <summary>
        ///  Формирование листа с искомой ожидающей книгой
        /// </summary>
        /// <param name="searchInfo" - строка, по которой ищутся совпадения></param>
        /// <returns>
        /// Лист с искомой ожидающей книгой
        /// </returns>
        public List<waiting> WaitingMatchUpInfoOutput(string searchInfo)
        {
            return dbHelper.context.waiting.Where(t => t.ticket.Contains(searchInfo)).ToList();
        }

        /// <summary>
        /// Добавление новой ожидающей книги
        /// </summary>
        /// <param name="userLogin" - логин></param>
        /// <param name="bookId" - идентификатор книги></param>
        /// <param name="ticket" - читательский билет></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddNewWaiting(string userLogin, int bookId, string ticket)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (userLogin == "" || bookId == 0 || !check.CheckTradingTicket(ticket))
                {
                    return false;
                }

                else
                {
                    dbHelper.context.waiting.Add(new waiting
                    {
                        login = userLogin,
                        book_id = bookId,
                        ticket = ticket,
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
        /// Форсмрование листа со всеми ожидающими книгами
        /// </summary>
        /// <returns>
        /// Лист со всеми ожидающими книгами
        /// </returns>
        public List<waiting> GetWaitingInfo()
        {
            return dbHelper.context.waiting.ToList();
        }

        /// <summary>
        /// Удаление ожидающей книги
        /// </summary>
        /// <param name="selectString" - Иднетификатор книги></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool DeleteEaitingBook(int selectString)
        {
            try
            {
                if(selectString == 0)
                {
                    return false;
                }
                else
                {
                    var selectWaiting = from search in dbHelper.context.waiting
                                        where search.waiting_id == selectString
                                        select search;

                    if (selectWaiting != null)
                    {
                        foreach (var item in selectWaiting)
                        {
                            dbHelper.context.waiting.Remove(item);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
