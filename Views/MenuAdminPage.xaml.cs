using LibraryProject.Controllers;
using Microsoft.Win32;
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
        readonly BooksController booksController = new BooksController();
        readonly ClientsController clientsController = new ClientsController();
        readonly FormularController formularController = new FormularController();

        /// <summary>
        /// Действия при инициализации страницы MenuAdminPage
        /// </summary>
        public MenuAdminPage()
        {
            InitializeComponent();
            BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
            ClientDataGrid.ItemsSource = clientsController.ClientsInfoOutputWithOutAdmin();
        }

        /// <summary>
        /// Событие при вводе текста в поле "SearchAdminBooksBox"
        /// </summary>

        private void SearchAdminBooksBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BookDataGrid.ItemsSource = booksController.BooksMatchUpInfoOutput(SearchAdminBooksBox.Text);
            if(SearchAdminBooksBox.Text == string.Empty)
            {
                BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "SearchAdminReadersBox"
        /// </summary>
        private void SearchAdminReadersBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClientDataGrid.ItemsSource = clientsController.ClientsMatchUpInfoOutput(SearchAdminReadersBox.Text);
            if (SearchAdminReadersBox.Text == string.Empty)
            {
                ClientDataGrid.ItemsSource = clientsController.ClientsInfoOutputWithOutAdmin();
            }
        }


        /// <summary>
        /// Событие при клике на кнопку "Удалить"
        /// </summary>
        private void DeleteClientBtn_Click(object sender, RoutedEventArgs e)
        {

            var item = ClientDataGrid.SelectedItem as Models.clients;

            if(ClientDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одного пользователя.");
            }

            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранного пользователя?", "Удаление пользователя", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (clientsController.DeleteClientInfo(item))
                    {
                        MessageBox.Show("Данные успешно удалены.");
                        ClientDataGrid.ItemsSource = clientsController.ClientsInfoOutputWithOutAdmin();
                    }
                    else
                    {
                        MessageBox.Show("Данные не были удалены, попробуйте позже.");
                    }
                }
                else
                {
                    this.NavigationService.Navigate(new Views.MenuAdminPage());

                }
            }
        }

        /// <summary>
        /// Событие при клике на кнопку "Добавить"
        /// </summary>
        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }

        /// <summary>
        /// Событие при клике на кнопку "Удалить"
        /// </summary>
        private void DelteBookBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = BookDataGrid.SelectedItem as Models.books;

            if (BookDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одной книги");
            }

            else
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную книгу?", "Удаление книги", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (booksController.DeleteBookInfo(item))
                    {
                        MessageBox.Show("Данные успешно удалены.");
                        BookDataGrid.ItemsSource = booksController.BooksInfoOutput();
                    }
                    else
                    {
                        MessageBox.Show("Данные не были удалены, попробуйте позже");
                    }
                }
                else
                {
                    this.NavigationService.Navigate(new Views.MenuAdminPage());

                }
            }
        }

        /// <summary>
        /// Событие при клике на кнопку "Добавить"
        /// </summary>
        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationPage());
        }
    }
}
