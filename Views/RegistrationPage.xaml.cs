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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        Controllers.ClientsController clientsController = new Controllers.ClientsController();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthorizationPage());
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            string resultString = "";

            StringCheck isName = new StringCheck();
            bool resultName = isName.CheckName(FirstNameInput.Text);
            if (!resultName)
            {
                resultString += "Неправильно введено Имя";
            }

            bool resultSurname = isName.CheckName(LastNameInput.Text);
            if (!resultSurname)
            {
                resultString += "Неправильно введена Фамилия";
            }

            bool resultPatronymic = isName.CheckName(PatronymicInput.Text);
            if (!resultPatronymic)
            {
                resultString += "Неправильно введено Отчество";
            }

            StringCheck isAddress = new StringCheck();
            bool resultAddress = isAddress.CheckAddress(AddressInput.Text);
            if (!resultAddress)
            {
                resultString += "Неправильно введен Адрес";
            }


            StringCheck isPhone = new StringCheck();
            bool resultPhone = isPhone.CheckPhone(PhoneInput.Text);
            if (!resultPhone)
            {
                resultString += "Неправильно введен Телефон";
            }

            StringCheck isLogin = new StringCheck();
            bool resultLogin = isLogin.CheckLogin(LoginInput.Text);
            if (!resultLogin)
            {
                resultString += "Неправильно введен Логин";
            }

            StringCheck isPassword = new StringCheck();
            bool resultPassword = isPassword.CheckPassword(PasswordInput.Password);
            if (!resultPassword)
            {
                resultString += "Неправильно введен Пароль";
            }

            if (resultString == "")
            {
                if(clientsController.CheckForAnExistingkUser(LoginInput.Text))
                {
                    clientsController.AddNewUser(FirstNameInput.Text, LastNameInput.Text, PatronymicInput.Text, Convert.ToDateTime(DateInput.SelectedDate), AddressInput.Text, WorkplaceInput.Text, StudyplaceInput.Text, PhoneInput.Text, LoginInput.Text, PasswordInput.Password);
                }

                else
                {
                    MessageBox.Show("Неккоректно введены данные");
                }
            }
            else
            {
                MessageBox.Show(resultString);
            }
        }

        private void FirstNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(FirstNameInput.Text);
            if (!trigger)
            {
                FirstNameWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                FirstNameWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void LastNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(LastNameInput.Text);
            if (!trigger)
            {
                LastNameWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                LastNameWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void PatronymicInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(PatronymicInput.Text);
            if (!trigger)
            {
                PatronymicWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                PatronymicWarningBtn.Visibility = Visibility.Collapsed;
            }
        }
    }
}
