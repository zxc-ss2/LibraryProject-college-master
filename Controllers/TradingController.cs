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

        public bool AddNewTrading(string bookId, string ticket, DateTime deliveryDate, DateTime receptionDate, Settings)
        {
            Settings.Default.login;
            dbHelper.context.SaveChanges();
            return true;
        }
    }
}
