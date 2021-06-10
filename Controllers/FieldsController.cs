using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public int SelectedIndexNewBBkInputComboBoxComboBox(books booksInfoDataContext, ComboBox newBBkInputComboBox)
        {
            try
            {
                var comboBoxItem = newBBkInputComboBox.Items.OfType<fields>().FirstOrDefault(x => x.field_knowledge_id == booksInfoDataContext.field_knowledge_id);
                int index = newBBkInputComboBox.SelectedIndex = newBBkInputComboBox.Items.IndexOf(comboBoxItem);
                return index;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

    }
}
