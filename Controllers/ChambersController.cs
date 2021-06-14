using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryProject.Controllers
{
    /// <summary>
    /// Класс для работы с данными таблицы "Chambers"
    /// </summary>
    public class ChambersController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        readonly DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Формирование листа со всеми отсекми
        /// </summary>
        /// <returns>
        /// Лист со всеми отсеками
        /// </returns>
        public List<chambers> GetChambers()
        {
            return dbHelper.context.chambers.ToList();
        }

        /// <summary>
        /// Вывод текущего индекса в комбо бокс
        /// </summary>
        /// <param name="booksInfoDataContext" - выбранная строка дата грид></param>
        /// <param name="newChamberComboBox" - изменяемый комбо бокс></param>
        /// <returns>
        /// index - в случае выполнения метода
        /// </returns>
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
