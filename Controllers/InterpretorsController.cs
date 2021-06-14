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
    /// Класс для работы с данными таблицы "Interpretors"
    /// </summary>
    public class InterpretorsController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Формирование листа со всеми издательствами
        /// </summary>
        /// <returns>
        /// Лист со всемми со всеми издательствами
        /// </returns>
        public List<interpretors> GetInterpretors()
        {
            return dbHelper.context.interpretors.ToList();
        }

        /// <summary>
        /// Поиск нужного номера издательства с помощью совпадений в поле interpreter_name
        /// </summary>
        /// <param name="userName" - строка, по которой ищутся совпадения></param>
        /// <returns>
        /// Икомый номер издательства
        /// </returns>
        public int GetInterpretorId(string userName)
        {
            return dbHelper.context.interpretors.Where(t => t.interpreter_name.Contains(userName)).First().interpreter_id;
        }

        /// <summary>
        /// Вывод текущего индекса в комбо бокс
        /// </summary>
        /// <param name="booksInfoDataContext" - выбранная строка дата грид></param>
        /// <param name="newInterpretorComboBox" - Изменяемый комбо бокс></param>
        /// <returns>
        /// index - в случае выполнения метода
        /// </returns>
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
