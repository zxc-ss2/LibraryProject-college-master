using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    class FieldsController
    {
        DbHelper dbHelper = new DbHelper();

        public List<Models.fields> GetBbk()
        {
            return dbHelper.context.fields.ToList();
        }

        public int GetBbkId(string selectedBbk)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_bbk == selectedBbk).First().field_knowledge_id;
        }

        public List<Models.fields> GetCorrectBbk(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).ToList();
        }
    }
}
