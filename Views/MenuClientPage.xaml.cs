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
    /// Логика взаимодействия для MenuClientPage.xaml
    /// </summary>
    public partial class MenuClientPage : Page
    {
        readonly BooksController booksController = new BooksController();
        readonly TradingController tradingController = new TradingController();
        readonly QuantityController quantityController = new QuantityController();
        readonly FormularController formularController = new FormularController();
        readonly ClientsController clientsController = new ClientsController();

        public MenuClientPage()
        {
            InitializeComponent();
            AllBooksDataGrid.ItemsSource = booksController.BooksInfoOutput();
            AvailableBooksDataGrid.ItemsSource = booksController.GetAvailableBooks(Settings.Default.login);
            ClientTakenBooksDataGrid.ItemsSource = booksController.GetTradingBooks();
        }
        List<quantity> booksQuantity = new List<quantity>();
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

        private void GetBookBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] strList = clientsController.GetTicket(Settings.Default.login).Split('-');

            if (AbonementsTypeList.Text == "А - только абонемент")
            {
                strList[0] = "А";
                ticket = strList[0] + "-" + strList[1] + "-" + strList[2];
            }

            if (AbonementsTypeList.Text == "Ч - только читальный зал")
            {
                strList[0] = "Ч";
                ticket = strList[0] + "-" + strList[1] + "-" + strList[2];
            }

            if (AbonementsTypeList.Text == "О - читальный зал и абонемент")
            {
                strList[0] = "О";
                ticket = strList[0] + "-" + strList[1] + "-" + strList[2];
            }

            clientsController.UpdateUserTicket(Settings.Default.login, ticket);
            if (string.IsNullOrEmpty(AbonementsTypeList.Text))
            {
                MessageBox.Show("Выберите вид абонемента");
            }
            else
            {
                var firstSelectedCellContent = new DataGridCellInfo(AvailableBooksDataGrid.SelectedItem, AvailableBooksDataGrid.Columns[0]);
                TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;


                if (firstSelectedCell == null)
                {
                    MessageBox.Show("Выберите книгу");
                }
                else
                {
                    booksQuantity = quantityController.GetQuantity(Convert.ToInt32(firstSelectedCell.Text));
                    if (tradingController.AddNewTrading(Convert.ToInt32(firstSelectedCell.Text), ticket, DateTime.Now, DateTime.Now.AddMonths(1), Settings.Default.login))
                    {
                        quantityController.ChangeQuantityMinus(Convert.ToInt32(firstSelectedCell.Text), booksQuantity);
                        booksController.AssignIdTradingToBook(Convert.ToInt32(firstSelectedCell.Text), tradingController.GetNeededTradingId(Convert.ToInt32(firstSelectedCell.Text)));
                        formularController.AddFormularInfo(ticket, DateTime.Now, DateTime.Now.AddMonths(1), Convert.ToInt32(firstSelectedCell.Text));
                        AvailableBooksDataGrid.ItemsSource = booksController.GetAvailableBooks(Settings.Default.login);
                        ClientTakenBooksDataGrid.ItemsSource = booksController.GetTradingBooks();
                    }
                }
            }
        }

        private void ReturnBookBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = new DataGridCellInfo(ClientTakenBooksDataGrid.SelectedItem, ClientTakenBooksDataGrid.Columns[0]);
            TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;
            var item = ClientTakenBooksDataGrid.SelectedItem as books;

            if (item == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки");
            }
            else
            {
                int zxc = Convert.ToInt32(item.trading_id);
                formularController.AddBookReturnDate(DateTime.Now, clientsController.GetTicket(Settings.Default.login));
                if (booksController.RemoveIdTradingFromBook(Convert.ToInt32(firstSelectedCell.Text)))
                {
                    booksQuantity = quantityController.GetQuantity(Convert.ToInt32(firstSelectedCell.Text));
                    if (tradingController.RemoveTrading(zxc))
                    {
                        AvailableBooksDataGrid.ItemsSource = booksController.GetAvailableBooks(Settings.Default.login);
                        ClientTakenBooksDataGrid.ItemsSource = booksController.GetTradingBooks();
                        quantityController.ChangeQuantityPlus(Convert.ToInt32(firstSelectedCell.Text), booksQuantity);
                        MessageBox.Show("Данные успешно обновлены");
                    }
                }              
            } 
        }
    }
}
