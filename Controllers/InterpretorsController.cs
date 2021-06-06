using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryProject.Controllers
{
    public class InterpretorsController
    {
        DbHelper dbHelper = new DbHelper();

        public List<interpretors> GetInterpretors()
        {
            return dbHelper.context.interpretors.ToList();
        }

        public int GetInterpretorId(string userName)
        {
            return dbHelper.context.interpretors.Where(t => t.interpreter_name.Contains(userName)).First().interpreter_id;
        }

        public string GetInterpretorNameWithId(int userName)
        {
            return dbHelper.context.interpretors.Where(t => t.interpreter_id == userName).First().interpreter_name;
        }

        public int SelectedIndexInterpretorComboBox(books booksInfoDataContext, ComboBox newInterpretorComboBox)
        {
            try
            {
                var comboBoxItem = newInterpretorComboBox.Items.OfType<interpretors>().FirstOrDefault(x => x.interpreter_id == booksInfoDataContext.interpreter_id);
                int index = newInterpretorComboBox.SelectedIndex = newInterpretorComboBox.Items.IndexOf(comboBoxItem);
                return index;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}
