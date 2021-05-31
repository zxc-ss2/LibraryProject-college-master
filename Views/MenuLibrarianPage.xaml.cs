using LibraryProject.Controllers;
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
    /// Логика взаимодействия для MenuLibrarianPage.xaml
    /// </summary>
    public partial class MenuLibrarianPage : Page
    {
        TradingController tradingController = new TradingController();
        BooksController booksController = new BooksController();
        public MenuLibrarianPage()
        {
            InitializeComponent();
            TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
            BookDataGrid.ItemsSource = booksController.BooksInfoOutput();

            
        }

        private void DeleteTradingInfoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTradingInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }

        private void DeleteBookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = BookDataGrid.SelectedItem as Models.books;

            if (item == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки");
            }
            else
            {
                booksController.DeleteBookInfo(item);
                BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
            }
        }

        private void AddBookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }

        private void EditBookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = new DataGridCellInfo(BookDataGrid.SelectedItem, BookDataGrid.Columns[0]);
            TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;

            Settings.Default.selectBook = Convert.ToInt32(firstSelectedCell.Text);

            this.NavigationService.Navigate(new EditBookPage());
        }

        private void EditTradingInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = new DataGridCellInfo(TradingDataGrid.SelectedItem, TradingDataGrid.Columns[0]);
            TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;

            if(firstSelectedCell == null)
            {
                MessageBox.Show("Выберите книгу из раздела 'мои книги'");
            }
            else
            {
                Settings.Default.selectBook2 = Convert.ToInt32(firstSelectedCell.Text);

                this.NavigationService.Navigate(new EditTradingPage());
            }
        }
    }
}
