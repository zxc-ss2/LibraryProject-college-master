using LibraryProject.Properties;
using LibraryProject.Views;
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

namespace LibraryProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Действия при инициализации главной страницы MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Views.AuthorizationPage());
        }

        public string login;
        public string password;

        /// <summary>
        /// Событие при клике на картинку "PersonalAreaImage"
        /// </summary>
        private void PersonalAreaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new Views.EditUserPage(password));
        }

        /// <summary>
        /// Событие при клике на картинку "Image"
        /// </summary>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is AuthorizationPage || e.Content is RegistrationPage)
            {
                BackIcon.Visibility = Visibility.Collapsed;
                PersonalAreaImage.Visibility = Visibility.Collapsed;
                LogOutBtn.Visibility = Visibility.Collapsed;
                ExitBtn.Visibility = Visibility.Visible;
            }
            else
            {
                BackIcon.Visibility = Visibility.Visible;
                PersonalAreaImage.Visibility = Visibility.Visible;
                LogOutBtn.Visibility = Visibility.Visible;
                ExitBtn.Visibility = Visibility.Collapsed;
            }

            if(e.Content is AuthorizationPage)
            {
                MainTitle.Text = "Авторизация";
            }

            if (e.Content is AddBookPage)
            {
                MainTitle.Text = "Добавление книги";
            }

            if (e.Content is EditBookPage)
            {
                MainTitle.Text = "Редактирование книги";
            }

            if (e.Content is EditTradingPage)
            {
                MainTitle.Text = "Редактирование выдачи";
            }

            if (e.Content is EditUserPage)
            {
                MainTitle.Text = "Редактирование аккаунта";
            }

            if (e.Content is MenuAdminPage)
            {
                MainTitle.Text = "Меню администратора";
            }

            if (e.Content is MenuClientPage)
            {
                MainTitle.Text = "Меню читателя";
            }

            if (e.Content is MenuLibrarianPage)
            {
                MainTitle.Text = "Меню библиотекаря";
            }

            if (e.Content is RegistrationPage)
            {
                MainTitle.Text = "Регистриция";
            }
        }

        /// <summary>
        /// Событие при клике на кнопку "Выйти"
        /// </summary>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Событие при клике на кнопку "Выйти"
        /// </summary>
        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход из аккаунта", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                MainFrame.Navigate(new Views.AuthorizationPage());
            }
        }
    }
}
