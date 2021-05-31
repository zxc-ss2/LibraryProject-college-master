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
   public class TradingController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<trading> GetTradingInfo()
        {
            return dbHelper.context.trading.ToList();
        }

        public List<trading> TradingInfoOutput()
        {
            return dbHelper.context.trading.Where(x=>x.login == "UliyaBay").ToList();
        }

        public bool AddNewTrading(int bookId, string ticket, DateTime deliveryDate, DateTime receptionDate, string userLogin)
        {
            //dbHelper.context.trading.Where(t => t.book_id)

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

        public int GetNeededTradingId(int selectBook)
        {
            return dbHelper.context.trading.Where(t => t.book_id == selectBook).First().trading_id;
        }

        public List<int> GetBooksId()
        {
            List<int> tradingBooksId = new List<int>();

            foreach (var item in dbHelper.context.trading.ToList())
            {
                tradingBooksId.Add(item.book_id.Value);
            }

            if(tradingBooksId != null)
            {
                return tradingBooksId;
            }
            else{
                return null;
            }
        }

        public List<trading> GetTradingString(int selectedBook)
        {
            return dbHelper.context.trading.Where(t => t.trading_id == selectedBook).ToList();
        }

        public bool UpdateTradingInfo(int newBook_id, string newTicket, DateTime newDelivery, DateTime newReception, List<trading> oldBook)
        {
            foreach (var item in oldBook)
            {
                item.book_id = newBook_id;
                item.ticket = newTicket;
                item.delivery = newDelivery;
                item.reception = newReception;
            }

            dbHelper.context.SaveChanges();
            return true;
        }

    }
}
