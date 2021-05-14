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
    /// Логика взаимодействия для MenuAdminPage.xaml
    /// </summary>
    public partial class MenuAdminPage : Page
    {
        Controllers.BooksController booksController = new Controllers.BooksController();
        Controllers.ClientsController clientsController = new Controllers.ClientsController();
        public MenuAdminPage()
        {
            InitializeComponent();
            BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
            ClientDataGrid.ItemsSource = clientsController.ClientsInfoOutput();
        }

        private void SearchAdminBooksBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookDataGrid.ItemsSource = booksController.BooksMatchUpInfoOutput(SearchAdminBooksBox.Text);
        }

        private void SearchAdminReadersBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClientDataGrid.ItemsSource = clientsController.ClientsMatchUpInfoOutput(SearchAdminReadersBox.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = ClientDataGrid.SelectedItem as Models.clients;

            if(item == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки");
            }
            else
            {
                clientsController.DeleteClientInfo(item);
                ClientDataGrid.ItemsSource = clientsController.ClientsInfoOutput();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var item = BookDataGrid.SelectedItem as Models.books;

            if (item == null)
            {
                MessageBox.Show("Вы не выбрали ни одной строки");
            }
            else
            {
                booksController.DeleteBookInfo(item);
                ClientDataGrid.ItemsSource = clientsController.ClientsInfoOutput();
            }
        }
    }
}
