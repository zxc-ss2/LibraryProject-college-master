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
        Controllers.BooksController booksController = new Controllers.BooksController();
        Controllers.FieldsController fieldsController = new Controllers.FieldsController();
        public AddBookPage()
        {
            InitializeComponent();
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            booksController.AddNewBook(AuthorInput.Text, fieldsController.GetBbkId(BBkInputComboBox.Text), NameInput.Text, ISBNInput.Text, PlaceInput.Text, Convert.ToInt32(YearInput.Text), Convert.ToInt32(InterpretrInput.Text), Convert.ToInt32(ChamberInput.Text));
        }

        private void BBkInputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                BBkInputComboBox.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && BBkInputComboBox.SelectedItem == null)
            {
                BBkInputComboBox.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }

            BBkInputComboBox    .IsDropDownOpen = true;
            if (BBkInputComboBox.SelectedItem == null)
            {
                // Если элемент не выбран менять фильтр
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(BBkInputComboBox.ItemsSource);
                cv.Filter = s => ((string)s).IndexOf(BBkInputComboBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
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
