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
        readonly WaitingController waitingController = new WaitingController();

        /// <summary>
        /// Действия при инициализации страницы MenuClientPage
        /// </summary>
        public MenuClientPage()
        {
            InitializeComponent();
            AllBooksDataGrid.ItemsSource = booksController.BooksInfoOutput();
            AvailableBooksDataGrid.ItemsSource = booksController.GetAvailableBooks(Settings.Default.login);
            ClientTakenBooksDataGrid.ItemsSource = booksController.GetTradingBooks();

            FilterList.SelectedIndex = 0;
        }
        List<quantity> booksQuantity = new List<quantity>();

        /// <summary>
        /// Событие при изменении значения поля "FilterList"
        /// </summary>
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

        /// <summary>
        /// Событие при клике на кнопку "Взять книгу"
        /// </summary>
        private void GetBookBtn_Click(object sender, RoutedEventArgs e)
        {
            string ticket = "";
            string generator = "";

            if (AbonementsTypeList.Text == "А - только абонемент")
            {
                if (clientsController.CheckTicketOnExistence(Settings.Default.login) != "")
                {
                    string[] strList = clientsController.GetTicketNumber(Settings.Default.login).Split('-');

                    strList[0] = "А";
                    ticket = strList[0] + "-" + strList[1] + "-" + strList[2];
                }
                else
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 4; i++)
                    {
                        generator += rnd.Next(0, 9);
                    }

                    ticket = "А" + "-" + generator + "-" + DateTime.Now.ToString("yy");
                    clientsController.UpdateUserTicket(Settings.Default.login, ticket);

                }
            }

            if (AbonementsTypeList.Text == "Ч - только читальный зал")
            {
                if (clientsController.CheckTicketOnExistence(Settings.Default.login) != "")
                {
                    string[] strList = clientsController.GetTicketNumber(Settings.Default.login).Split('-');

                    strList[0] = "Ч";
                    ticket = strList[0] + "-" + strList[1] + "-" + strList[2];
                }
                else
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 4; i++)
                    {
                        generator += rnd.Next(0, 9);
                    }

                    ticket = "Ч" + "-" + generator + "-" + DateTime.Now.ToString("yy");
                    clientsController.UpdateUserTicket(Settings.Default.login, ticket);

                }
            }

            if (AbonementsTypeList.Text == "О - читальный зал и абонемент")
            {
                if (clientsController.CheckTicketOnExistence(Settings.Default.login) != "")
                {
                    string[] strList = clientsController.GetTicketNumber(Settings.Default.login).Split('-');

                    strList[0] = "О";
                    ticket = strList[0] + "-" + strList[1] + "-" + strList[2];
                }
                else
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 4; i++)
                    {
                        generator += rnd.Next(0, 9);
                    }

                    ticket = "О" + "-" + generator + "-" + DateTime.Now.ToString("yy");
                    clientsController.UpdateUserTicket(Settings.Default.login, ticket);

                }
            }

            if (string.IsNullOrEmpty(AbonementsTypeList.Text))
            {
                MessageBox.Show("Выберите вид абонемента");
            }
            else
            {
                if (AvailableBooksDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Выберите книгу");
                }
                else
                {
                    var firstSelectedCellContent = new DataGridCellInfo(AvailableBooksDataGrid.SelectedItem, AvailableBooksDataGrid.Columns[0]);
                    TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;

                    Settings.Default.userSelecredBookId = Convert.ToInt32(firstSelectedCell.Text);
                    if (waitingController.AddNewWaiting(Settings.Default.login, Convert.ToInt32(firstSelectedCell.Text), ticket))
                    {
                        MessageBox.Show("Ваш запрос на получение будет обработан в ближайшее время");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                    }
                }
            }
        }
    }
}
