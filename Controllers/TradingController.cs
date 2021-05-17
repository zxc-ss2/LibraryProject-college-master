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
    }
}
