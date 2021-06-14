using LibraryProject.Controllers;
using LibraryProject.Models;
using LibraryProject.Properties;
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
    /// Логика взаимодействия для EditTradingPage.xaml
    /// </summary>
    public partial class EditTradingPage : Page
    {
        readonly TradingController tradingController = new TradingController();
        readonly BooksController booksController = new BooksController();
        readonly List<trading> updatingTrading = new List<trading>();
        public EditTradingPage(LibraryEntities context, trading bookDataContext)
        {
            InitializeComponent();

            NewBookComboBox.DisplayMemberPath = "book_id";
            NewBookComboBox.SelectedValuePath = "book_id";

            NewBookComboBox.ItemsSource = booksController.BooksInfoOutput();
            NewBookComboBox.SelectedIndex = booksController.SelectedIndexBookComboBox(bookDataContext, NewBookComboBox);

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {
                NewTicketInput.Text = item.ticket;
                NewDeliveryInput.Text = Convert.ToString(item.delivery.ToString("yyyy.MM.dd"));
                NewReceptionInput.Text = Convert.ToString(item.reception.ToString("yyyy.MM.dd"));
            }

            updatingTrading = tradingController.GetTradingString(Settings.Default.selectBook2);
        }


        private void NewTicketInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();
            bool trigger = check.CheckTradingTicket(NewTicketInput.Text);

            string word = NewTicketInput.Text;

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (word != Convert.ToString(item.ticket) && word != "" && trigger)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookComboBox.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewDeliveryInput_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckDate(Convert.ToString(NewDeliveryInput.SelectedDate.Value.Date.ToString("yyyy.MM.dd")));
            bool word = check.CheckDate(Convert.ToString(Convert.ToDateTime(NewDeliveryInput.Text).ToString("yyyy.MM.dd")));
            string word2 = Convert.ToDateTime(NewDeliveryInput.Text).ToString("yyyy.MM.dd");
            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (trigger && word2 != Convert.ToString(item.delivery.ToString("yyyy.MM.dd")) && word2 != "" && word)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookComboBox.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewBookComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var word = NewBookComboBox.SelectedItem as books;

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (word.book_id != item.book_id && NewBookComboBox.Text != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookComboBox.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }


        private void NewReceptionInput_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckDate(Convert.ToString(NewReceptionInput.SelectedDate.Value.Date.ToString("yyyy.MM.dd")));
            bool word = check.CheckDate(Convert.ToString(Convert.ToDateTime(NewReceptionInput.Text).ToString("yyyy.MM.dd")));
            string word2 = Convert.ToDateTime(NewReceptionInput.Text).ToString("yyyy.MM.dd");
            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (trigger && word2 != Convert.ToString(item.reception.ToString("yyyy.MM.dd")) && word2 != "" && word)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookComboBox.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var book = NewBookComboBox.SelectedItem as books;
            if (tradingController.UpdateTradingInfo(Convert.ToInt32(book.book_id.ToString()), NewTicketInput.Text, Convert.ToDateTime(NewDeliveryInput.SelectedDate), Convert.ToDateTime(NewReceptionInput.SelectedDate), updatingTrading))
            {
                SaveBtn.IsEnabled = false;
                MessageBox.Show("Данные успешно обновлены");
                this.NavigationService.Navigate(new MenuLibrarianPage());
            }
            else
            {
                MessageBox.Show("Данные не были обновлены");
            }
        }
    }
}
