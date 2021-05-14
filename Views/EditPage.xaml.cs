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
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {

        Controllers.ClientsController clientsController = new Controllers.ClientsController();
        List<Models.clients> zxc = new List<Models.clients>();

        public EditPage(string password)
        {
            InitializeComponent();

            foreach (var item in clientsController.ClientsPasswordMatchUp(password))
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
            }

            zxc = clientsController.ClientsPasswordMatchUp(password);
        }

        private void FirstNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewFirstNameInput.Text);
            string word = NewFirstNameInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if(NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void LastNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewLastNameInput.Text);
            string word = NewLastNameInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void PatronymicInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(NewPatronymicInput.Text);
            string word = NewPatronymicInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void AddressInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckAddress(NewAddressInput.Text);
            string word = NewAddressInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void WorkplaceInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            string word = NewWorkplaceInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
            {

                if (word != item.workplace)
                {
                    SaveBtn.IsEnabled = true;
                }
                else
                {
                    SaveBtn.IsEnabled  = false;
                }
            }

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void StudyplaceInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            string word = NewStudyplaceInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void PhoneInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckPhone(NewPhoneInput.Text);
            string word = NewPhoneInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void LoginInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckLogin(NewLoginInput.Text);
            string word = NewLoginInput.Text;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckPassword(NewPasswordInput.Password);
            string word = NewPasswordInput.Password;

            foreach (var item in clientsController.ClientsPasswordMatchUp(NewPasswordInput.Password))
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

            if (NewFirstNameInput.Text == "" || NewLastNameInput.Text == "" || NewPatronymicInput.Text == "" || NewAddressInput.Text == "" || NewPhoneInput.Text == "" || NewLoginInput.Text == "" || NewPasswordInput.Password == "")
            {
                SaveBtn.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(clientsController.UpdateClientInfo(NewFirstNameInput.Text, NewLastNameInput.Text, NewPatronymicInput.Text, NewAddressInput.Text, NewWorkplaceInput.Text, NewStudyplaceInput.Text, NewPhoneInput.Text, NewLoginInput.Text, NewPasswordInput.Password, zxc))
            {
                SaveBtn.IsEnabled = false;
                MessageBox.Show("Данные успешно обновлены");
            }
            else
            {
                MessageBox.Show("Данные не были обновлены");
            }
        }


    }
}
