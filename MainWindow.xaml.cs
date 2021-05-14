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
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Views.AuthorizationPage());
        }

        public string login;
        public string password;

        private void PersonalAreaImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new Views.EditPage(password));
        }

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
                PersonalAreaImage.Visibility = Visibility.Collapsed;
                LogOutBtn.Visibility = Visibility.Collapsed;
                ExitBtn.Visibility = Visibility.Visible;
            }
            else
            {
                PersonalAreaImage.Visibility = Visibility.Visible;
                LogOutBtn.Visibility = Visibility.Visible;
                ExitBtn.Visibility = Visibility.Collapsed;
            }

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

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
