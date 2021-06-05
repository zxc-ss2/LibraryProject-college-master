using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    public class ChambersController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<chambers> GetChambers()
        {
            return dbHelper.context.chambers.ToList();
        }
    }
}
