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
        TradingController tradingController = new TradingController();
        BooksController booksController = new BooksController();
        BbkCheckClass bbkCheckClass = new BbkCheckClass(); 
        QuantityController quantityController = new QuantityController(); 
        public MenuLibrarianPage()
        {
            InitializeComponent();
            TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
            BookDataGrid.ItemsSource = booksController.BooksInfoOutput();

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


                if (booksController.RemoveIdTradingFromBook(Convert.ToInt32(secondSelectedCell.Text)))
                {
                    booksQuantity = quantityController.GetQuantity(Convert.ToInt32(secondSelectedCell.Text));
                    if (tradingController.RemoveTrading(tradingId))
                    {
                        if (quantityController.ChangeQuantityPlus(Convert.ToInt32(secondSelectedCell.Text), booksQuantity))
                        {
                            TradingDataGrid.ItemsSource = tradingController.GetTradingInfo();
                            MessageBox.Show("Данные успешно обновлены");
                        }
                        else
                        {
                            MessageBox.Show("Количетсво не было перезаписано");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Возврат не был произведен, попробуйте позже");
                    }
                }
                else
                {
                    MessageBox.Show("Возврат не был произведен, попробуйте позже");
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
                    BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
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
                this.NavigationService.Navigate(new EditBookPage());
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
    }
}
