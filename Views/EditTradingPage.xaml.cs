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
        readonly List<trading> updatingTrading = new List<trading>();
        public EditTradingPage()
        {
            InitializeComponent();

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {
                NewBookIdInput.Text = Convert.ToString(item.book_id);
                NewTicketInput.Text = item.ticket;
                NewDeliveryInput.Text = Convert.ToString(item.delivery);
                NewReceptionInput.Text = Convert.ToString(item.reception);
            }

            updatingTrading = tradingController.GetTradingString(Settings.Default.selectBook2);
        }

        private void NewBookIdInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewBookIdInput.Text;

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (word != Convert.ToString(item.book_id) && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookIdInput.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
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

            if (NewBookIdInput.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewDeliveryInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewDeliveryInput.Text;

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (word != Convert.ToString(item.delivery) && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookIdInput.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void NewReceptionInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = NewReceptionInput.Text;

            foreach (var item in tradingController.GetTradingString(Settings.Default.selectBook2))
            {

                if (word != Convert.ToString(item.reception) && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewBookIdInput.Text == "" || NewTicketInput.Text == "" || NewDeliveryInput.Text == "" || NewReceptionInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tradingController.UpdateTradingInfo(Convert.ToInt32(NewBookIdInput.Text), NewTicketInput.Text, Convert.ToDateTime(NewDeliveryInput.Text), Convert.ToDateTime(NewReceptionInput.Text), updatingTrading))
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
