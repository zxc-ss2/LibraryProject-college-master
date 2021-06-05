using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    public class FieldsController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<fields> GetBbk()
        {
            return dbHelper.context.fields.ToList();
        }

        public List<string> GetBbkNumbers()
        {
            List<string> bbk = new List<string>();
            foreach (var item in dbHelper.context.fields)
            {
                bbk.Add(item.field_knowledge_bbk);
            }

            return bbk;
        }

        public int GetBbkId(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).First().field_knowledge_id;
        }

        public List<fields> GetCorrectBbk(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).ToList();
        }

    }
}
