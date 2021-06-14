using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
    /// <summary>
    /// Класс для работы с данными таблицы "Formular"
    /// </summary>
    public class FormularController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Добавление нового формуляра
        /// </summary>
        /// <param name="ticket" - Читательский билет></param>
        /// <param name="dateDelivery" - Дата выдачи></param>
        /// <param name="dateReception" - Максимальная дата возварата></param>
        /// <param name="bookId" - номер книги></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddFormularInfo(string ticket, DateTime dateDelivery, DateTime dateReception, int bookId)
        {
            try
            {
                if(bookId == 0 || ticket == null)
                {
                    return false;
                }
                else
                {
                    dbHelper.context.formular.Add(new formular
                    {
                        ticket = ticket,
                        book_delivery = dateDelivery,
                        book_reception = dateReception,
                        book_return = null,
                        book_id = bookId
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
        /// Добавление фактической даты возврата книги читателем в формуляр
        /// </summary>
        /// <param name="returnDate" - фактическая дата возврата></param>
        /// <param name="userTicket" - билет читателя, который вернул книгу></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddBookReturnDate(DateTime returnDate, string userTicket)
        {
            try
            {
                if(returnDate == null)
                {
                    return false;
                }
                else
                {
                    var formularList = dbHelper.context.formular.Where(t => t.ticket == userTicket).ToList();

                    foreach (var item in formularList)
                    {
                        item.book_return = returnDate;
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

        /// <summary>
        /// Формирование csv файла со всеми формулярами
        /// </summary>
        /// <param name="nameFile"></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool FormularFile(string nameFile)
        {
            try
            {
                int check = dbHelper.context.formular.ToList().Count();
                if (check == 0 || string.IsNullOrEmpty(nameFile))
                {
                    return false;
                }

                else
                {
                    DateTime deliveryDate = Convert.ToDateTime(dbHelper.context.formular.FirstOrDefault().book_delivery);
                    DateTime receptionDate = Convert.ToDateTime(dbHelper.context.formular.FirstOrDefault().book_reception);
                    DateTime returnBook = Convert.ToDateTime(dbHelper.context.formular.FirstOrDefault().book_return);
                    string ticket = dbHelper.context.formular.FirstOrDefault().ticket;

                    using (StreamWriter wr = new StreamWriter(nameFile, false, Encoding.UTF8))
                    {
                        foreach (var item in dbHelper.context.formular.ToList())
                        {
                            wr.WriteLine($" Дата выдачи:;  {item.book_delivery}");
                            wr.WriteLine($" Максимальная дата возврата:;  {item.book_reception}");
                            wr.WriteLine($" Фактическая дата возврата:;  {item.book_return}");
                            wr.WriteLine($" Номер чит. билета:;  {item.ticket}");
                        }
                    }
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
