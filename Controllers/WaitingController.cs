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
    class WaitingController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<waiting> WaitingMatchUpInfoOutput(string searchInfo)
        {
            return dbHelper.context.waiting.Where(t => t.ticket.Contains(searchInfo)).ToList();
        }

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

        public List<waiting> GetWaitingInfo()
        {
            return dbHelper.context.waiting.ToList();
        }

        public bool DeleteEaitingBook(int selectString)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
