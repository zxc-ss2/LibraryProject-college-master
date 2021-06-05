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
        readonly BooksController booksController = new BooksController();
        readonly ChambersController chambersController = new ChambersController();
        readonly InterpretorsController interpretorsController = new InterpretorsController();
        readonly List<books> updatingBook = new List<books>();
        public EditBookPage()
        {
            InitializeComponent();
            NewChamberComboBox.ItemsSource = chambersController.GetChambers();
            NewChamberComboBox.DisplayMemberPath = "chamber_id";
            NewChamberComboBox.SelectedValuePath = "chamber_id";

            NewInterpreterComboBox.ItemsSource = interpretorsController.GetInterpretors();
            NewInterpreterComboBox.DisplayMemberPath = "interpreter_name";
            NewInterpreterComboBox.SelectedValuePath = "interpreter_id";

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {
                NewAuthorInput.Text = item.author;
                NewNameInput.Text = item.name;
                NewBbkInput.Text = Convert.ToString(item.field_knowledge_id);
                NewIsbnInput.Text = item.isbn;
                NewPlaceInput.Text = item.place;
                NewYearInput.Text = Convert.ToString(item.year);
                NewInterpreterComboBox.Text = Convert.ToString(item.interpreter_id);
                NewChamberComboBox.Text = Convert.ToString(item.chamber_id);
            }

            updatingBook = booksController.GetBookWithId(Settings.Default.selectBook);
        }

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewBbkInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewBbkInput.Text;

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewInterpreterInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewInterpreterComboBox.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.interpreter_id) && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewChamberInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewChamberComboBox.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.chamber_id))
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterComboBox.Text == "" || NewChamberComboBox.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (booksController.UpdateBookInfo(NewAuthorInput.Text, Convert.ToInt32(NewBbkInput.Text), NewNameInput.Text, NewIsbnInput.Text, NewPlaceInput.Text, Convert.ToInt32(NewYearInput.Text), Convert.ToInt32(NewInterpreterComboBox.Text), Convert.ToInt32(NewChamberComboBox.Text), updatingBook))
            {
                SaveBtn.IsEnabled = false;
                MessageBox.Show("Данные успешно обновлены");
            }
            else
            {
                MessageBox.Show("Данные не были обновлены");
            }
        }
    }
}
