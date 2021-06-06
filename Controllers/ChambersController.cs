using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryProject.Controllers
{
    public class ChambersController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<chambers> GetChambers()
        {
            return dbHelper.context.chambers.ToList();
        }

        public int SelectedIndexChamberComboBox(books booksInfoDataContext, ComboBox newChamberComboBox)
        {
            try
            {
                var comboBoxItem = newChamberComboBox.Items.OfType<chambers>().FirstOrDefault(x => x.chamber_id == booksInfoDataContext.chamber_id);
                int index = newChamberComboBox.SelectedIndex = newChamberComboBox.Items.IndexOf(comboBoxItem);
                return index;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}
