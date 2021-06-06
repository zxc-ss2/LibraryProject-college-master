using LibraryProject.Controllers;
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
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        readonly BooksController booksController = new BooksController();
        readonly FieldsController fieldsController = new FieldsController();
        readonly ChambersController chambersController = new ChambersController();
        readonly InterpretorsController interpretorsController = new InterpretorsController();
        public AddBookPage()
        {
            InitializeComponent();
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";

            ChamberComboBox.ItemsSource = chambersController.GetChambers();
            ChamberComboBox.DisplayMemberPath = "chamber_id";
            ChamberComboBox.SelectedValuePath = "chamber_id";

            InterpreterComboBox.ItemsSource = interpretorsController.GetInterpretors();
            InterpreterComboBox.DisplayMemberPath = "interpreter_name";
            InterpreterComboBox.SelectedValuePath = "interpreter_id";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringCheck check = new StringCheck();
            string resultString = "";

            bool resultAuthor = check.CheckName(AuthorInput.Text);
            if (!resultAuthor || AuthorInput.Text == "")
            {
                resultString += "Неправильно введено имя Автора ";
            }

            bool resultName = check.CheckBookName(NameInput.Text);
            if (!resultName || NameInput.Text == "")
            {
                resultString += "Неправильно введено название ";
            }

            string resultBbk = BBkInputComboBox.Text;
            if (resultBbk == "")
            {
                resultString += "Неправильно введено BBK ";
            }

            bool resultIsbn = check.CheckBookIsbn(ISBNInput.Text);
            if (!resultIsbn || ISBNInput.Text == "")
            {
                resultString += "Неправильно введен ISBN ";
            }

            bool resultYear = check.CheckBookYear(YearInput.Text);
            if (!resultYear || YearInput.Text == "")
            {
                resultString += "Неправильно введен год ";
            }

            string resultInterpretor = InterpreterComboBox.Text;
            if (resultInterpretor == "")
            {
                resultString += "Неправильно введено имя Издания ";
            }

            if(resultString == "")
            {
                if (booksController.AddNewBook(AuthorInput.Text, fieldsController.GetBbkId(BBkInputComboBox.Text), NameInput.Text, ISBNInput.Text, PlaceInput.Text, Convert.ToInt32(YearInput.Text), interpretorsController.GetInterpretorId(InterpreterComboBox.Text), Convert.ToInt32(ChamberComboBox.Text)))
                {
                    MessageBoxResult result = MessageBox.Show("Вернуться на страницу добавления?", "Книга добавлена", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.No) 
                    {
                        this.NavigationService.Navigate(new MenuAdminPage());
                    }
                    else
                    {
                        this.NavigationService.Navigate(new AddBookPage());
                    }
                }
            }
            else
            {
                MessageBox.Show(resultString);
            }
            
        }

        private void BbkSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (BbkSearchBox.Text != null)
                {
                    BBkInputComboBox.ItemsSource = fieldsController.GetCorrectBbk(BbkSearchBox.Text);
                    BBkInputComboBox.IsDropDownOpen = true;
                }
            }
            catch
            {
                throw new Exception("Ошибка базы данных, попробуйте позже.");
            }


        }

        private void RadioButtonBbk_Click(object sender, RoutedEventArgs e)
        {
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";
        }

        private void RadioButtonName_Click(object sender, RoutedEventArgs e)
        {
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_name";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";
        }

        private void DirectInputBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectInputBtn.Visibility = Visibility.Visible;
            DirectInputBtn.Visibility = Visibility.Collapsed;
            DirectInputTextBox.Visibility = Visibility.Visible;
            BBkInputComboBox.Visibility = Visibility.Collapsed;
            SelectShowDocPanel.Visibility = Visibility.Collapsed;
        }

        private void SelectInputBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectInputBtn.Visibility = Visibility.Collapsed;
            DirectInputBtn.Visibility = Visibility.Visible;
            DirectInputTextBox.Visibility = Visibility.Collapsed;
            BBkInputComboBox.Visibility = Visibility.Visible;
            SelectShowDocPanel.Visibility = Visibility.Visible;
        }
    }
}
