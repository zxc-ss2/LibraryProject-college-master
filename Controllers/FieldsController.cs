using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    class FieldsController
    {
        readonly Models.DbHelper dbHelper = new Models.DbHelper();

        public List<Models.fields> GetBbk()
        {
            return dbHelper.context.fields.ToList();
        }

        //public int GetBbkId(string selectedBbk)
        //{
        //    return dbHelper.context.fields.Where(t => t.field_knowledge_bbk == selectedBbk || t.field_knowledge_name == selectedBbk).First().field_knowledge_bbk;
        //}

        public int GetBbkId(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).First().field_knowledge_id;
        }

        public List<Models.fields> GetCorrectBbk(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).ToList();
        }

        //public string ConversionBbkNameToBbkId(string selectedBbkName)
        //{
        //    return dbHelper.context.fields.Where(t => t.field_knowledge_bbk == selectedBbkId || t.field_knowledge_name == selectedBbkName).First().field_knowledge_bbk;
        //}
    }
}
