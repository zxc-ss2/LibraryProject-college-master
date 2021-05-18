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
    }
}
