using LibraryProject.Models;
using LibraryProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBbkLib
{
    public class CheckBbk
    {
        DbHelper dbHelper = new DbHelper();
        FieldsController fieldsController = new FieldsController();
        public bool CheckBbkString(string bbk)
        {

            foreach (var item in fieldsController.GetBbkNumbers())
            {
                if (item.Contains(bbk))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
