using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    public class InterpretorsController
    {
        DbHelper dbHelper = new DbHelper();

        public List<interpretors> GetInterpretors()
        {
            return dbHelper.context.interpretors.ToList();
        }
    }
}
