using LibraryProject.Controllers;
using LibraryProject.Models;
using LibraryProject.Properties;
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
        readonly ClientsController clientsController = new ClientsController();
        readonly BooksController booksController = new BooksController();
        readonly List<books> UpdatingBook = new List<books>();
        public EditBookPage()
        {
            InitializeComponent();

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {
                NewAuthorInput.Text = item.author;
                NewNameInput.Text = item.name;
                NewBbkInput.Text = Convert.ToString(item.field_knowledge_id);
                NewIsbnInput.Text = item.isbn;
                NewPlaceInput.Text = item.place;
                NewYearInput.Text = Convert.ToString(item.year);
                NewQuantityIdInput.Text = Convert.ToString(item.quantity_id);
                NewStorageIdInput.Text = Convert.ToString(item.storage_id);
                NewInterpreterInput.Text = Convert.ToString(item.interpreter_id);
                NewChamberInput.Text = Convert.ToString(item.chamber_id);
            }

            UpdatingBook = booksController.GetBookWithId(Settings.Default.selectBook);
        }

        private void NewAuthorInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewAuthorInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.author && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewNameInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.name && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewIsbnInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewIsbnInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != item.isbn && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewYearInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewYearInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.year) && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewQuantityIdInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewQuantityIdInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.quantity_id))
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewStorageIdInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewStorageIdInput.Text;

            foreach (var item in booksController.GetBookWithId(Settings.Default.selectBook))
            {

                if (word != Convert.ToString(item.storage_id))
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewInterpreterInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewInterpreterInput.Text;

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewChamberInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewChamberInput.Text;

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

            if (NewAuthorInput.Text == "" || NewNameInput.Text == "" || NewBbkInput.Text == "" || NewIsbnInput.Text == "" || NewYearInput.Text == "" || NewInterpreterInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (booksController.UpdateBookInfo(NewAuthorInput.Text, Convert.ToInt32(NewBbkInput.Text), NewNameInput.Text, NewIsbnInput.Text, NewPlaceInput.Text, Convert.ToInt32(NewYearInput.Text), Convert.ToInt32(NewQuantityIdInput.Text), Convert.ToInt32(NewStorageIdInput.Text), Convert.ToInt32(NewInterpreterInput.Text), Convert.ToInt32(NewChamberInput.Text), UpdatingBook))
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
