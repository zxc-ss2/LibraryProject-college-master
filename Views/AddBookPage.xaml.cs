using LibraryProject.Controllers;
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
        public AddBookPage()
        {
            InitializeComponent();
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(booksController.AddNewBook(AuthorInput.Text, fieldsController.GetBbkId(BBkInputComboBox.Text), NameInput.Text, ISBNInput.Text, PlaceInput.Text, Convert.ToInt32(YearInput.Text), Convert.ToInt32(InterpretrInput.Text), Convert.ToInt32(ChamberInput.Text)))
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

        private void BBkInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BbkSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BBkInputComboBox.ItemsSource = fieldsController.GetCorrectBbk(BbkSearchBox.Text);
            BBkInputComboBox.IsDropDownOpen = true;
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
