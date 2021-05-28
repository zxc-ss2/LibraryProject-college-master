using LibraryProject.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
   public class TradingController
    {
        DbHelper dbHelper = new DbHelper();

        public List<Models.trading> TradingInfoOutput()
        {
            return dbHelper.context.trading.Where(x=>x.login == "UliyaBay").ToList();
        }

        public bool AddNewTrading(int bookId, string ticket, DateTime deliveryDate, DateTime receptionDate, string userLogin)
        {
            //dbHelper.context.trading.Where(t => t.book_id)

            dbHelper.context.trading.Add(new Models.trading
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
            List<int> zxc = new List<int>();

            foreach (var item in dbHelper.context.trading.ToList())
            {
                zxc.Add(item.book_id.Value);
            }

            if(zxc != null)
            {
                return zxc;
            }
            else{
                return null;
            }
        }
    }
}
