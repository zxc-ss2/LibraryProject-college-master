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
    /// Логика взаимодействия для MenuClientPage.xaml
    /// </summary>
    public partial class MenuClientPage : Page
    {
        readonly Controllers.BooksController booksController = new Controllers.BooksController();
        readonly Controllers.TradingController tradingController = new Controllers.TradingController();
        readonly Controllers.QuantityController quantityController = new Controllers.QuantityController();

        public MenuClientPage()
        {
            InitializeComponent();
            AllBooksDataGrid.ItemsSource = booksController.BooksInfoOutput();
            AvailableBooksDataGrid.ItemsSource = booksController.GetAvailableBooks(Settings.Default.login);
        }
        List<Models.quantity> booksQuantity = new List<Models.quantity>();
        List<Models.books> books = new List<Models.books>();
        string ticket = "";
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
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void GetBookClick(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string generator = "";

            if (AbonementsTypeList.Text == "А - только абонемент")
            {
                for (int i = 0; i < 4; i++)
                {
                    generator += rnd.Next(0, 9);
                }

                ticket = "А" + generator + "-" + DateTime.Now.ToString("yy");
            }

            if (AbonementsTypeList.Text == "Ч - только читальный зал")
            {
                for (int i = 0; i < 4; i++)
                {
                    generator += rnd.Next(0, 9);
                }

                ticket = "Ч" + generator + "-" + DateTime.Now.ToString("yy");
            }

            if (AbonementsTypeList.Text == "О - читальный зал и абонемент")
            {
                for (int i = 0; i < 4; i++)
                {
                    generator += rnd.Next(0, 9);
                }

                ticket = "О" + generator + "-" + DateTime.Now.ToString("yy");
            }

            if (string.IsNullOrEmpty(AbonementsTypeList.Text))
            {
                MessageBox.Show("Выберите вид абонемента");
            }

            var firstSelectedCellContent = new DataGridCellInfo(AvailableBooksDataGrid.SelectedItem, AvailableBooksDataGrid.Columns[0]);
            TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;

            

            if(firstSelectedCell == null)
            {
                MessageBox.Show("Выберите книгу");
            }
            else
            {
                booksQuantity = quantityController.GetQuantity(Convert.ToInt32(firstSelectedCell.Text));
                books = booksController.BooksInfoOutput();
                if (tradingController.AddNewTrading(Convert.ToInt32(firstSelectedCell.Text), ticket, DateTime.Now, DateTime.Now.AddMonths(1), Settings.Default.login))
                {
                    quantityController.ChangeQuantity(Convert.ToInt32(firstSelectedCell.Text), booksQuantity);
                    booksController.AssignIdTradingToBook(Convert.ToInt32(firstSelectedCell.Text), tradingController.GetNeededTradingId(Convert.ToInt32(firstSelectedCell.Text)), books);
                }
            }

        }

    }
}
