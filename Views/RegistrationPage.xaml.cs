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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        readonly Controllers.ClientsController clientsController = new Controllers.ClientsController();

        /// <summary>
        /// Действия при инициализации страницы RegistrationPage
        /// </summary>
        public RegistrationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Событие при клике на кнопку "Войти"
        /// </summary>
        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthorizationPage());
        }

        /// <summary>
        /// Событие при вводе текста в поле "NewNameInput"
        /// </summary>
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

        /// <summary>
        /// Событие при вводе текста в поле "LastNameInput"
        /// </summary>
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

        /// <summary>
        /// Событие при вводе текста в поле "PatronymicInput"
        /// </summary>
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

        /// <summary>
        /// Событие при вводе текста в поле "AddressInput"
        /// </summary>
        private void AddressInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckAddress(Convert.ToString(AddressInput.Text));
            if (!trigger)
            {
                AddressWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AddressWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "PhoneInput"
        /// </summary>
        private void PhoneInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckPhone(Convert.ToString(PhoneInput.Text));
            if (!trigger)
            {
                PhoneWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                PhoneWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "LoginInput"
        /// </summary>
        private void LoginInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckLogin(Convert.ToString(LoginInput.Text));
            if (!trigger)
            {
                LoginWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                LoginWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "PasswordInput"
        /// </summary>
        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckPassword(Convert.ToString(PasswordInput.Password));
            if (!trigger)
            {
                PasswordWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        ///  Событие при клике на кнопку "Продолжить"
        /// </summary>
        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            string generator = "";
            string resultString = "";

            StringCheck isName = new StringCheck();
            bool resultName = isName.CheckName(FirstNameInput.Text);
            if (!resultName)
            {
                resultString += "Неправильно введено Имя\n";
            }

            bool resultSurname = isName.CheckName(LastNameInput.Text);
            if (!resultSurname)
            {
                resultString += "Неправильно введена Фамилия\n";
            }

            bool resultPatronymic = isName.CheckName(PatronymicInput.Text);
            if (!resultPatronymic)
            {
                resultString += "Неправильно введено Отчество\n";
            }

            if(DateInput.SelectedDate == null)
            {
                resultString += "Неправильно введена Дата\n";
            }
            else
            {
                bool resultDate = isName.CheckDate(DateInput.SelectedDate.Value.Date.ToString("yyyy.MM.dd"));
                bool resultDate2 = isName.CheckDate(Convert.ToString(Convert.ToDateTime(DateInput.Text).ToString("yyyy.MM.dd")));
                if (!resultDate || !resultDate2)
                {
                    resultString += "Неправильно введена Дата\n";
                }
            }

            StringCheck isAddress = new StringCheck();
            bool resultAddress = isAddress.CheckAddress(AddressInput.Text);
            if (!resultAddress)
            {
                resultString += "Неправильно введен Адрес\n";
            }


            StringCheck isPhone = new StringCheck();
            bool resultPhone = isPhone.CheckPhone(PhoneInput.Text);
            if (!resultPhone)
            {
                resultString += "Неправильно введен Телефон\n";
            }

            StringCheck isLogin = new StringCheck();
            bool resultLogin = isLogin.CheckLogin(LoginInput.Text);
            if (!resultLogin)
            {
                resultString += "Неправильно введен Логин\n";
            }

            StringCheck isPassword = new StringCheck();
            bool resultPassword = isPassword.CheckPassword(PasswordInput.Password);
            if (!resultPassword)
            {
                resultString += "Неправильно введен Пароль\n";
            }

            StringCheck isEmail = new StringCheck();
            bool resultEmail = isEmail.CheckEmail(EmailInput.Text);
            if (!resultEmail)
            {
                resultString += "Неправильно введен email\n";
            }



            if (resultString == "")
            {
                if(clientsController.CheckForAnExistingkUser(LoginInput.Text))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        generator += rnd.Next(0, 9);
                    }

                    FirstNameInput.IsReadOnly = true;
                    LastNameInput.IsReadOnly = true;
                    PatronymicInput.IsReadOnly = true;
                    AddressInput.IsReadOnly = true;
                    WorkplaceInput.IsReadOnly = true;
                    StudyplaceInput.IsReadOnly = true;
                    PhoneInput.IsReadOnly = true;
                    LoginInput.IsReadOnly = true;
                    EmailInput.IsReadOnly = true;

                    string ticket = "X" + "-" + generator + "-" + DateTime.Now.ToString("yy");
                    DateTime word2 = Convert.ToDateTime(Convert.ToDateTime(DateInput.Text).ToString("yyyy.MM.dd"));
                    if (clientsController.AddNewUser(FirstNameInput.Text, LastNameInput.Text, PatronymicInput.Text, word2, AddressInput.Text, WorkplaceInput.Text, StudyplaceInput.Text, PhoneInput.Text, LoginInput.Text, PasswordInput.Password, EmailInput.Text, ticket))
                    {
                        MessageBox.Show("Регистрация прошла успешно.");

                        if(clientsController.SendInfo(LoginInput.Text, PasswordInput.Password, EmailInput.Text))
                        {
                            MessageBox.Show("Данные для авторизации были отправлены на вашу электронную почту. Если письмо не пришло, убедитесь, что оно не попало в папку Спам.");
                            this.NavigationService.Navigate(new AuthorizationPage());
                        }
                        else
                        {
                            MessageBox.Show("Данные для авторизации не были отправлены, повторная попытка будет произведена позже.");
                            FirstNameInput.IsReadOnly = false;
                            LastNameInput.IsReadOnly = false;
                            PatronymicInput.IsReadOnly = false;
                            AddressInput.IsReadOnly = false;
                            WorkplaceInput.IsReadOnly = false;
                            StudyplaceInput.IsReadOnly = false;
                            PhoneInput.IsReadOnly = false;
                            LoginInput.IsReadOnly = false;
                            EmailInput.IsReadOnly = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка регистрации");
                    }
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

        
    }
}
