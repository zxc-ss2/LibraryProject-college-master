using LibraryProject.Models;
using LibraryProject.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
    /// <summary>
    /// Класс для работы с данными таблицы "Quantity"
    /// </summary>
    public class QuantityController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        readonly DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Формирование листа с количеством выбранной книги
        /// </summary>
        /// <param name="selectBook"></param>
        /// <returns>
        /// Лист с количеством выбранной книги
        /// </returns>
        public List<quantity> GetQuantity(int selectBook)
        {
            return dbHelper.context.quantity.Where(t => t.book_id == selectBook).ToList();
        }

        /// <summary>
        /// Добавление нового счетчика количества
        /// </summary>
        /// <param name="newBook" - идентификатор книги></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddNewQuantity(int newBook)
        {
            try
            {
                Random rnd = new Random();
                dbHelper.context.quantity.Add(new quantity
                {
                    book_id = newBook,
                    library_quantity = rnd.Next(0, 15)
                });
                dbHelper.context.SaveChanges();
                Settings.Default.quantityId = dbHelper.context.quantity.OrderByDescending(t => t.quantity_id).FirstOrDefault().quantity_id;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Изменение количества выбранной книги на -1
        /// </summary>
        /// <param name="selectBook" - Идентификатор выбранной книги></param>
        /// <param name="bookQuantityList" - Лист со старыми данными></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool ChangeQuantityMinus(int selectBook, List<quantity> bookQuantityList)
        {
            try
            {
                if (selectBook == 0)
                {
                    return false;
                }
                else
                {
                    var book = dbHelper.context.quantity.Where(t => t.book_id == selectBook).First().library_quantity;

                    foreach (var item in bookQuantityList)
                    {
                        item.library_quantity = book -= 1;
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
        /// Изменеие количества выбранной книги на +1
        /// </summary>
        /// <param name="selectBook" - Идентификатор выбранной книги></param>
        /// <param name="bookQuantityList" - Лист со старыми данными></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool ChangeQuantityPlus(int selectBook, List<quantity> bookQuantityList)
        {
            try
            {
                if(selectBook == 0)
                {
                    return false;
                }
                else
                {
                    var book = dbHelper.context.quantity.Where(t => t.book_id == selectBook).First().library_quantity;

                    foreach (var item in bookQuantityList)
                    {
                        item.library_quantity = book += 1;
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
