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
        public bool CheckBbk(string bbk)
        {
            //List<char> delimiterChars = new List<char>();
            //delimiterChars.Add(',');
            //delimiterChars.Add(';');

            //List<string> bbkParts = new List<string>();
            //bbkParts.Add(bbk.Split((",")));

            char[] dilimiterChars = {',','.'};
            string[] bbkParts = bbk.Split(dilimiterChars);

            //foreach (var item in fieldsController.GetBbkNumbers())
            //{

            //}
            return true;
        }
    }
}
