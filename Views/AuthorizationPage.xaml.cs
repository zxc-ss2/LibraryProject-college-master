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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        readonly ClientsController clientsController = new ClientsController();

        /// <summary>
        /// Действия при инициализации страницы AuthorizationPage
        /// </summary>
        public AuthorizationPage()
        {
            InitializeComponent();

            LoginTextBox.Text = String.Empty;
            PasswordTextBox.Text = String.Empty;
        }

        /// <summary>
        /// Событие при клике на кнопку "Зарегистрироваться"
        /// </summary>
        private void SignUpBtnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationPage());
        }

        /// <summary>
        /// Событие при клике на кнопку "Войти"
        /// </summary>
        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            if (clientsController.CheckUser(LoginTextBox.Text, PasswordTextBox.Text))
            {
                if(Settings.Default.role == 1)
                {
                    this.NavigationService.Navigate(new MenuAdminPage());

                    MainWindow page = (MainWindow)Application.Current.MainWindow;
                    page.login = LoginTextBox.Text;
                    page.password = PasswordTextBox.Text;
                }

                else if (Settings.Default.role == 2)
                {
                    this.NavigationService.Navigate(new MenuLibrarianPage());

                    MainWindow page = (MainWindow)Application.Current.MainWindow;
                    page.login = LoginTextBox.Text;
                    page.password = PasswordTextBox.Text;
                }

                else if (Settings.Default.role == 3)
                {
                    this.NavigationService.Navigate(new MenuClientPage());

                    MainWindow page = (MainWindow)Application.Current.MainWindow;
                    page.login = LoginTextBox.Text;
                    page.password = PasswordTextBox.Text;
                }
            }
        }
    }
}
