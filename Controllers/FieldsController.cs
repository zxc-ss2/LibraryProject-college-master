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
    /// Класс для работы с данными таблицы "Fields"
    /// </summary>
    public class FieldsController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        readonly DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Формирование листа со всеми ббк
        /// </summary>
        /// <returns>
        /// Лист со всеми ббк
        /// </returns>
        public List<fields> GetBbk()
        {
            return dbHelper.context.fields.ToList();
        }

        /// <summary>
        /// Формирование листа со всеми номерами ббк
        /// </summary>
        /// <returns>
        /// Лист со всеми номерами ббк
        /// </returns>
        public List<string> GetBbkNumbers()
        {
            List<string> bbk = new List<string>();
            foreach (var item in dbHelper.context.fields)
            {
                bbk.Add(item.field_knowledge_bbk);
            }

            return bbk;
        }


        /// <summary>
        /// Поиск нужного идентификатора ббк с помощью совпадений полей field_knowledge_name, field_knowledge_bbk с вводимой строкой
        /// </summary>
        /// <param name="userField" - строка, по которой ищутся совпадения></param>
        /// <returns>
        /// Искомый идентификатор ббк
        /// </returns>
        public int GetBbkId(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).First().field_knowledge_id;
        }

        /// <summary>
        /// Поиск совпадений полей field_knowledge_name, field_knowledge_bbk с вводимой строкой
        /// </summary> 
        /// <param name="userField" - строка, по которой ищутся совпадения></param>
        /// <returns>
        /// Лист с совпадениями
        /// </returns>
        public List<fields> GetCorrectBbk(string userField)
        {
            return dbHelper.context.fields.Where(t => t.field_knowledge_name.Contains(userField) || t.field_knowledge_bbk.Contains(userField)).ToList();
        }

        /// <summary>
        /// Вывод текущего индекса в комбо бокс
        /// </summary>
        /// <param name="booksInfoDataContext" - выбранная строка дата грид></param>
        /// <param name="newBBkInputComboBox" - изменяемый комбо бокс></param>
        /// <returns>
        /// index - в случае выполнения метода
        /// </returns>
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
