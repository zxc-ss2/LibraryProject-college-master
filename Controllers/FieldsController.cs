using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    class FieldsController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<fields> GetBbk()
        {
            return dbHelper.context.fields.ToList();
        }

        public List<string> GetBbkNumbers()
        {
            List<string> bbk = new List<string>();
            //foreach (var item in dbHelper.context.fields)
            //{
            //    string element = dbHelper.context.fields.First().field_knowledge_bbk;
            //    bbk[item] = element;
            //}

            for (int i = 0; i < dbHelper.context.fields.Count(); i++)
            {
                string element = dbHelper.context.fields.Select(t => t.field_knowledge_id == i);
                bbk.Add(element);
            }

            return bbk;
        }
        //public int GetBbkId(string selectedBbk)
        //{
        //    return dbHelper.context.fields.Where(t => t.field_knowledge_bbk == selectedBbk || t.field_knowledge_name == selectedBbk).First().field_knowledge_bbk;
        //}

        public int GetBbkId(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).First().field_knowledge_id;
        }

        public List<fields> GetCorrectBbk(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).ToList();
        }

        //public string ConversionBbkNameToBbkId(string selectedBbkName)
        //{
        //    return dbHelper.context.fields.Where(t => t.field_knowledge_bbk == selectedBbkId || t.field_knowledge_name == selectedBbkName).First().field_knowledge_bbk;
        //}
    }
}
