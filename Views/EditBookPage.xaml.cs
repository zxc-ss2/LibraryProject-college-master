using LibraryProject.Controllers;
using LibraryProject.Models;
using LibraryProject.Properties;
using StringCheckLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryProject.Views
{
    /// <summary>
    /// Логика взаимодействия для EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Page
    {
        readonly DbHelper dbHelper = new DbHelper();
        readonly BooksController booksController = new BooksController();
        readonly ChambersController chambersController = new ChambersController();
        readonly InterpretorsController interpretorsController = new InterpretorsController();
        readonly FieldsController fieldsController = new FieldsController();
        readonly List<books> updatingBook = new List<books>();

        /// <summary>
        /// Действия при инициализации страницы EditBookPage
        /// </summary>
        /// <param name="context" - контекст базы данных></param>
        /// <param name="bookDataContext" - выбранная строка дата грид></param>
        public EditBookPage(LibraryEntities context, books bookDataContext)
        {
            InitializeComponent();
            NewInterpreterComboBox.DisplayMemberPath = "interpreter_name";
            NewInterpreterComboBox.SelectedValuePath = "interpreter_id";

            NewChamberComboBox.DisplayMemberPath = "chamber_id";
            NewInterpreterComboBox.SelectedValuePath = "chamber_id";

            NewBBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            NewBBkInputComboBox.SelectedValuePath = "field_knowledge_id";

            NewChamberComboBox.ItemsSource = chambersController.GetChambers();
            NewChamberComboBox.SelectedIndex = chambersController.SelectedIndexChamberComboBox(bookDataContext, NewChamberComboBox);

            NewInterpreterComboBox.ItemsSource = interpretorsController.GetInterpretors();
            NewInterpreterComboBox.SelectedIndex = interpretorsController.SelectedIndexInterpretorComboBox(bookDataContext, NewInterpreterComboBox);

            NewBBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            NewBBkInputComboBox.SelectedIndex = fieldsController.SelectedIndexNewBBkInputComboBoxComboBox(bookDataContext, NewBBkInputComboBox);
            updatingBook = booksController.GetBookWithId(Settings.Default.selectBook);

            foreach (var item in updatingBook)
            {
                NewAuthorInput.Text = item.author;
                NewNameInput.Text = item.name;
                NewIsbnInput.Text = item.isbn;
                NewPlaceInput.Text = item.place;
                NewYearInput.Text = Convert.ToString(item.year);
            }

        }

        /// <summary>
        /// Событие при клике на кнопку "Ввести вручную"
        /// </summary>
        private void DirectInputBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectInputBtn.Visibility = Visibility.Visible;
            DirectInputBtn.Visibility = Visibility.Collapsed;
            DirectInputTextBox.Visibility = Visibility.Visible;
            NewBBkInputComboBox.Visibility = Visibility.Collapsed;
            SelectShowDocPanel.Visibility = Visibility.Collapsed;
            BbkSearchBox.Visibility = Visibility.Collapsed;
            BbkPlaceholderTextBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Событие при клике на кнопку "Вернуться к списку"
        /// </summary>
        private void SelectInputBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectInputBtn.Visibility = Visibility.Collapsed;
            DirectInputBtn.Visibility = Visibility.Visible;
            DirectInputTextBox.Visibility = Visibility.Collapsed;
            NewBBkInputComboBox.Visibility = Visibility.Visible;
            SelectShowDocPanel.Visibility = Visibility.Visible;
            BbkSearchBox.Visibility = Visibility.Visible;
            BbkPlaceholderTextBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewAuthorInput"
        /// </summary>
        private void NewAuthorInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewAuthorInput.Text);
            string word = NewAuthorInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.author && word != "" && trigger)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewNameInput"
        /// </summary>
        private void NewNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckBookName(NewNameInput.Text);
            string word = NewNameInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.name && word != "" && trigger)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewBbkInput"
        /// </summary>
        private void NewBbkInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewBBkInputComboBox.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.field_knowledge_id) && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewIsbnInput"
        /// </summary>
        private void NewIsbnInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckBookIsbn(NewIsbnInput.Text);
            string word = NewIsbnInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.isbn && word != "" && trigger)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewPlaceInput"
        /// </summary>
        private void NewPlaceInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewPlaceInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.place)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewYearInput"
        /// </summary>
        private void NewYearInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckBookYear(NewYearInput.Text);
            string word = NewYearInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.year) && word != "" && trigger)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при выборе нового значения в поле "NewInterpreterComboBox"
        /// </summary>
        private void NewInterpreterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var word = NewInterpreterComboBox.SelectedItem as interpretors;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word.interpreter_id != item.interpreter_id && NewInterpreterComboBox.Text != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при выборе нового значения в поле "NewChamberComboBox"
        /// </summary>
        private void NewChamberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var word = NewChamberComboBox.SelectedItem as chambers;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word.chamber_id != item.chamber_id)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при выборе нового значения в поле "NewBBkInputComboBox"
        /// </summary>
        private void NewBBkInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var word = NewBBkInputComboBox.SelectedItem as fields;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word.field_knowledge_id != item.field_knowledge_id)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "DirectInputTextBox"
        /// </summary>
        private void DirectInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<string> zxc = fieldsController.GetBbkNumbers();
            string word = DirectInputTextBox.Text;

            if (zxc.Contains(word))
            {
                BbkWarningBtn.Visibility = Visibility.Collapsed;
                foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
                {

                    if (word != item.fields.field_knowledge_bbk && word != "")
                    {
                        SaveBtn.IsEnabled = true;
                    }
                    else
                    {
                        SaveBtn.IsEnabled = false;
                    }
                }
            }
            else
            {
                BbkWarningBtn.Visibility = Visibility.Visible;
                SaveBtn.IsEnabled = false;
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBBkInputComboBox.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }


        /// <summary>
        /// Событие при клике на кнопку "Продолжить"
        /// </summary>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var chamber = NewChamberComboBox.SelectedItem as chambers;
            var interpretor = NewInterpreterComboBox.SelectedItem as interpretors;
            var field = NewBBkInputComboBox.SelectedItem as fields;

            int userBbk = 0;

            if (SelectInputBtn.Visibility == Visibility.Collapsed)
            {
                BbkWarningBtn.Visibility = Visibility.Collapsed;
                userBbk = Convert.ToInt32(NewBBkInputComboBox.SelectedValue);
            }
            else if(SelectInputBtn.Visibility == Visibility.Visible)
            {
                userBbk = fieldsController.GetBbkId(DirectInputTextBox.Text);
            }

            if (booksController.UpdateBookInfo(NewAuthorInput.Text, userBbk, NewNameInput.Text, NewIsbnInput.Text, NewPlaceInput.Text, Convert.ToInt32(NewYearInput.Text), Convert.ToInt32(interpretor.interpreter_id.ToString()), Convert.ToInt32(chamber.chamber_id.ToString()), updatingBook))
            {
                SaveBtn.IsEnabled = false;
                MessageBox.Show("Данные успешно обновлены");
                if(Settings.Default.role == 1)
                {
                    this.NavigationService.Navigate(new MenuAdminPage());
                }
                if(Settings.Default.role == 2)
                {
                    this.NavigationService.Navigate(new MenuLibrarianPage());
                }
            }
            else
            {
                MessageBox.Show("Данные не были обновлены");
            }
        }
    }
}
