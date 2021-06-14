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
    public class QuantityController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<quantity> GetQuantity(int selectBook)
        {
            return dbHelper.context.quantity.Where(t => t.book_id == selectBook).ToList();
        }

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
