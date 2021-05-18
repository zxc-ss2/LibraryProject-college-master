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
    /// Логика взаимодействия для MenuClientPage.xaml
    /// </summary>
    public partial class MenuClientPage : Page
    {
        Controllers.BooksController booksController = new Controllers.BooksController();
        Controllers.TradingController tradingController = new Controllers.TradingController();

        public MenuClientPage()
        {
            InitializeComponent();
            AllBooksDataGrid.ItemsSource = booksController.BooksInfoOutput();
            AvailableBooksDataGrid.ItemsSource = booksController.GetAvailableBooks();
        }

        private void FilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FilterList.Text == "Мои книги")
            {
                ClientTakenBooksDataGrid.Visibility = Visibility.Collapsed;
                AllBooksDataGrid.Visibility = Visibility.Visible;
                AllBooksDataGrid.ItemsSource = booksController.BooksInfoOutput();

            }

            else if (FilterList.Text == "Все книги")
            {
                AllBooksDataGrid.Visibility = Visibility.Collapsed;
                ClientTakenBooksDataGrid.Visibility = Visibility.Visible;
                ClientTakenBooksDataGrid.ItemsSource = booksController.GetTradingBooks();
            }

        }

        private void GetBookClick(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = this.AvailableBooksDataGrid.Columns[0].GetCellContent(this.AvailableBooksDataGrid.SelectedItem);
            var firstSelectedCell = firstSelectedCellContent != null ? firstSelectedCellContent.Parent as DataGridCell : null;

        }
    }
}
