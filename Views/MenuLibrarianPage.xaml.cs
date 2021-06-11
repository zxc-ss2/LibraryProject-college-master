using LibraryProject.Classes;
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
    /// Логика взаимодействия для MenuLibrarianPage.xaml
    /// </summary>
    public partial class MenuLibrarianPage : Page
    {
        DbHelper dbHelper = new DbHelper();
        TradingController tradingController = new TradingController();
        BooksController booksController = new BooksController();
        BbkCheckClass bbkCheckClass = new BbkCheckClass(); 
        QuantityController quantityController = new QuantityController();
        ClientsController clientsController = new ClientsController(); 
        WaitingController waitingController = new WaitingController(); 
        FormularController formularController = new FormularController(); 
        public MenuLibrarianPage()
        {
            InitializeComponent();
            TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
            BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
            TradingClientsGrid.ItemsSource = clientsController.GetClientsWithTrading();
            WaitingBooksDataGrid.ItemsSource = waitingController.GetWaitingInfo();
        }

        private void DeleteTradingInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var secondSelectedCellContent = new DataGridCellInfo(TradingDataGrid.SelectedItem, TradingDataGrid.Columns[1]);

            if(TradingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной выдачи");
            }

            else
            {
                List<quantity> booksQuantity = new List<quantity>();
                TextBlock secondSelectedCell = secondSelectedCellContent.Column.GetCellContent(secondSelectedCellContent.Item) as TextBlock;
                var item = TradingDataGrid.SelectedItem as trading;
                int tradingId = Convert.ToInt32(item.trading_id);

                var lastSelectedCellContent = new DataGridCellInfo(TradingDataGrid.SelectedItem, TradingDataGrid.Columns[5]);
                TextBlock selectedLogin = lastSelectedCellContent.Column.GetCellContent(lastSelectedCellContent.Item) as TextBlock;

                if (booksController.RemoveIdTradingFromBook(Convert.ToInt32(secondSelectedCell.Text)))
                {
                    if (clientsController.RemoveIdTradingFromClient(selectedLogin.Text))
                    {
                        booksQuantity = quantityController.GetQuantity(Convert.ToInt32(secondSelectedCell.Text));
                        string ticket = tradingController.GetNeededTicket(tradingId);
                        if (formularController.AddBookReturnDate(DateTime.Now, ticket))
                        {
                            if (tradingController.RemoveTrading(tradingId))
                            {
                                if (quantityController.ChangeQuantityPlus(Convert.ToInt32(secondSelectedCell.Text), booksQuantity))
                                {
                                    TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
                                    BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
                                    TradingClientsGrid.ItemsSource = clientsController.GetClientsWithTrading();
                                    WaitingBooksDataGrid.ItemsSource = waitingController.GetWaitingInfo();
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                    }
                }
                else
                {
                     MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                }
            }
            
        }

        private void AddTradingInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }

        private void DeleteBookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = BookDataGrid.SelectedItem as Models.books;

            if (BookDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки");
            }
            else
            {
                if (booksController.DeleteBookInfo(item))
                {
                    MessageBox.Show("Данные успешно удалены.");
                    TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
                    BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
                    TradingClientsGrid.ItemsSource = clientsController.GetClientsWithTrading();
                    WaitingBooksDataGrid.ItemsSource = waitingController.GetWaitingInfo();
                }
                else
                {
                    MessageBox.Show("Данные не были удалены, попробуйте позже.");
                }
            }
        }

        private void AddBookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }

        private void EditBookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = new DataGridCellInfo(BookDataGrid.SelectedItem, BookDataGrid.Columns[0]);

            if(BookDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной книги");
            }
            else
            {
                TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;
                Settings.Default.selectBook = Convert.ToInt32(firstSelectedCell.Text);
                var updateBook = BookDataGrid.SelectedItem as books;
                this.NavigationService.Navigate(new EditBookPage(dbHelper.context, updateBook));
            }
        }

        private void EditTradingInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = new DataGridCellInfo(TradingDataGrid.SelectedItem, TradingDataGrid.Columns[0]);
            if(TradingDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной выдачи");
            }
            else
            {
                TextBlock firstSelectedCell = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;
                Settings.Default.selectBook2 = Convert.ToInt32(firstSelectedCell.Text);

                this.NavigationService.Navigate(new EditTradingPage());
            }
        }

        private void ApproveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(WaitingBooksDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбрано ни одной книги");
            }
            else
            {
                List<quantity> booksQuantity = new List<quantity>();

                var firstSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[0]);
                TextBlock selectedWaiting = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;
                var secondSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[1]);
                TextBlock selectedBook = secondSelectedCellContent.Column.GetCellContent(secondSelectedCellContent.Item) as TextBlock;
                var thirdSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[2]);
                TextBlock selectedTicket = thirdSelectedCellContent.Column.GetCellContent(thirdSelectedCellContent.Item) as TextBlock;
                var fourSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[3]);
                TextBlock selectedLogin = fourSelectedCellContent.Column.GetCellContent(fourSelectedCellContent.Item) as TextBlock;

                if (tradingController.AddNewTrading(Convert.ToInt32(selectedBook.Text), selectedTicket.Text, DateTime.Now, DateTime.Now.AddMonths(1), selectedLogin.Text))
                {
                    booksQuantity = quantityController.GetQuantity(Convert.ToInt32(selectedBook.Text));
                    if (quantityController.ChangeQuantityMinus(Convert.ToInt32(selectedBook.Text), booksQuantity))
                    {

                        if (booksController.AssignIdTradingToBook(Convert.ToInt32(selectedBook.Text), tradingController.GetNeededTradingId(Convert.ToInt32(selectedBook.Text))))
                        {
                            if (clientsController.AddTradingIdToCLient(selectedLogin.Text, tradingController.GetNeededTradingId(Convert.ToInt32(selectedBook.Text))))
                            {
                                if (formularController.AddFormularInfo(selectedTicket.Text, DateTime.Now, DateTime.Now.AddMonths(1), Convert.ToInt32(selectedBook.Text)))
                                {
                                    if (waitingController.DeleteEaitingBook(Convert.ToInt32(selectedWaiting.Text)))
                                    {
                                        WaitingBooksDataGrid.ItemsSource = waitingController.GetWaitingInfo();
                                        MessageBox.Show("Книга была выдана пользователю:" + selectedLogin.Text);
                                        TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
                                        BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
                                        TradingClientsGrid.ItemsSource = clientsController.GetClientsWithTrading();
                                        WaitingBooksDataGrid.ItemsSource = waitingController.GetWaitingInfo();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ошибка базы данных попроьуйте позже.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Формуляр не был заполнен");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Номер обмена не был присвоен выбранной книге((");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Количетсво не было перезаписано");
                    }
                }
                else
                {
                    MessageBox.Show("Выдача не была произведена, попробуйте позже");
                }
            }
        }

        private void DeniedBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[0]);
            TextBlock selectedWaiting = firstSelectedCellContent.Column.GetCellContent(firstSelectedCellContent.Item) as TextBlock;
            var secondSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[1]);
            TextBlock selectedBook = secondSelectedCellContent.Column.GetCellContent(secondSelectedCellContent.Item) as TextBlock;
            var thirdSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[2]);
            TextBlock selectedTicket = thirdSelectedCellContent.Column.GetCellContent(thirdSelectedCellContent.Item) as TextBlock;
            var fourSelectedCellContent = new DataGridCellInfo(WaitingBooksDataGrid.SelectedItem, WaitingBooksDataGrid.Columns[3]);
            TextBlock selectedLogin = fourSelectedCellContent.Column.GetCellContent(fourSelectedCellContent.Item) as TextBlock;

            if (WaitingBooksDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбрано ни одной книги");
            }
            else
            {
                if (waitingController.DeleteEaitingBook(Convert.ToInt32(selectedWaiting.Text)))
                {
                    WaitingBooksDataGrid.ItemsSource = waitingController.GetWaitingInfo();
                    MessageBox.Show("Книга была выдана пользователю:" + selectedLogin);
                }
                else
                {
                    MessageBox.Show("Ошибка базы данных попроьуйте позже.");
                }
            }
        }

        private void SearchLibrarianReadersBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TradingClientsGrid.ItemsSource = clientsController.ClientsMatchUpInfoOutput(SearchLibrarianReadersBox.Text);
        }

        private void SearchLibrarianBooksBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookDataGrid.ItemsSource = booksController.BooksMatchUpInfoOutput(SearchLibrarianBooksBox.Text);
        }

        private void SearchLibrarianTradingBooksBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TradingDataGrid.ItemsSource = booksController.BooksMatchUpInfoOutput(SearchLibrarianTradingBooksBox.Text);
        }

        private void SearchLibrarianWaitingBooksBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WaitingBooksDataGrid.ItemsSource = waitingController.WaitingMatchUpInfoOutput(SearchLibrarianWaitingBooksBox.Text);
        }
    }
}
