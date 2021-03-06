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
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        readonly ClientsController clientsController = new ClientsController();
        readonly List<clients> sessionClient = new List<clients>();

        /// <summary>
        /// Действия при инициализации страницы EditUserPage
        /// </summary>
        /// <param name="password" - пароль авторизованного пользователя></param>
        public EditUserPage(string password)
        {
            InitializeComponent();

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {
                NewFirstNameInput.Text = item.name;
                NewLastNameInput.Text = item.surname;
                NewPatronymicInput.Text = item.patronymic;
                NewDateInput.SelectedDate = Convert.ToDateTime(item.birthday);
                NewAddressInput.Text = item.address;
                NewWorkplaceInput.Text = item.workplace;
                NewStudyplaceInput.Text = item.studyplace;
                NewPhoneInput.Text = item.phone;
                NewLoginInput.Text = item.login;
                NewPasswordInput.Password = item.password;
                NewEmailInput.Text = item.email;
            }

            sessionClient = clientsController.ClientsLoginMatchUp(Settings.Default.login);
        }

        /// <summary>
        /// Событие при вводе текста в поле "FirstNameInput"
        /// </summary>
        private void FirstNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewFirstNameInput.Text);
            string word = NewFirstNameInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.name && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "" )
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "LastNameInput"
        /// </summary>
        private void LastNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewLastNameInput.Text);
            string word = NewLastNameInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.surname && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "PatronymicInput"
        /// </summary>
        private void PatronymicInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewPatronymicInput.Text);
            string word = NewPatronymicInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.patronymic && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "AddressInput"
        /// </summary>
        private void AddressInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckAddress(NewAddressInput.Text);
            string word = NewAddressInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.address && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "WorkplaceInput"
        /// </summary>
        private void WorkplaceInput_TextChanged(object sender, TextChangedEventArgs e)
        {

            string word = NewWorkplaceInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (word != item.workplace)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "StudyplaceInput"
        /// </summary>
        private void StudyplaceInput_TextChanged(object sender, TextChangedEventArgs e)
        {

            string word = NewStudyplaceInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (word != item.studyplace)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "PhoneInput"
        /// </summary>
        private void PhoneInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckPhone(NewPhoneInput.Text);
            string word = NewPhoneInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.phone && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "LoginInput"
        /// </summary>
        private void LoginInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckLogin(NewLoginInput.Text);
            string word = NewLoginInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.login && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "PasswordInput"
        /// </summary>
        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckPassword(NewPasswordInput.Password);
            string word = NewPasswordInput.Password;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.password && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewEmailInput"
        /// </summary>
        private void NewEmailInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckEmail(NewEmailInput.Text);
            string word = NewEmailInput.Text;

            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word != item.email && word != "")
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при выборе новой даты в поле "NewDateInput"
        /// </summary>
        private void NewDateInput_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckDate(Convert.ToString(NewDateInput.SelectedDate.Value.Date.ToString("yyyy.MM.dd")));
            bool word = check.CheckDate(Convert.ToString(Convert.ToDateTime(NewDateInput.Text).ToString("yyyy.MM.dd")));
            string word2 = Convert.ToDateTime(NewDateInput.Text).ToString("yyyy.MM.dd");
            foreach (var item in clientsController.ClientsLoginMatchUp(Settings.Default.login))
            {

                if (trigger && word2 != Convert.ToString(item.birthday.ToString("yyyy.MM.dd")) && word2 != "" && word)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" ||
                NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "" || NewEmailInput.Text == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        /// <summary>
        /// Событие при клике на кнопку "Продолжить"
        /// </summary>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clientsController.UpdateClientInfo(NewFirstNameInput.Text, NewLastNameInput.Text,  NewPatronymicInput.Text, Convert.ToDateTime(NewDateInput.SelectedDate), NewAddressInput.Text, NewWorkplaceInput.Text, NewStudyplaceInput.Text, NewPhoneInput.Text, NewLoginInput.Text, NewPasswordInput.Password, NewEmailInput.Text, sessionClient))
            {
                SaveBtn.IsEnabled = false;
                MessageBox.Show("Данные успешно обновлены");
                if (Settings.Default.role == 1)
                {
                    this.NavigationService.Navigate(new AuthorizationPage());
                }
                if (Settings.Default.role == 2)
                {
                    this.NavigationService.Navigate(new AuthorizationPage());
                }
                if (Settings.Default.role == 3)
                {
                    this.NavigationService.Navigate(new AuthorizationPage());
                }
            }
            else
            {
                MessageBox.Show("Данные не были обновлены");
            }
        }
    }
}
