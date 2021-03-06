using LibraryProject.Controllers;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Classes
{
    class BbkCheckClass
    {
        FieldsController fieldsController = new FieldsController();
        public bool CheckBb2k(string bbk)
        {
            //List<char> delimiterChars = new List<char>();
            //delimiterChars.Add(',');
            //delimiterChars.Add(';');

            //List<string> bbkParts = new List<string>();

            foreach (var item in fieldsController.GetBbkNumbers())
            {
                if (item.Contains(bbk))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
