using LibraryProject.Controllers;
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
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        readonly BooksController booksController = new BooksController();
        readonly FieldsController fieldsController = new FieldsController();
        readonly ChambersController chambersController = new ChambersController();
        readonly InterpretorsController interpretorsController = new InterpretorsController();
        readonly QuantityController quantityController = new QuantityController();

        /// <summary>
        /// Действия при инициализации страницы AddBookPage
        /// </summary>
        public AddBookPage()
        {
            InitializeComponent();
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";

            ChamberComboBox.ItemsSource = chambersController.GetChambers();
            ChamberComboBox.DisplayMemberPath = "chamber_id";
            ChamberComboBox.SelectedValuePath = "chamber_id";

            InterpreterComboBox.ItemsSource = interpretorsController.GetInterpretors();
            InterpreterComboBox.DisplayMemberPath = "interpreter_name";
            InterpreterComboBox.SelectedValuePath = "interpreter_id";
        }

        /// <summary>
        /// Событие при вводе текста в поле "BbkSearchBox"
        /// </summary>
        private void BbkSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (BbkSearchBox.Text != null)
                {
                    BBkInputComboBox.ItemsSource = fieldsController.GetCorrectBbk(BbkSearchBox.Text);
                    BBkInputComboBox.IsDropDownOpen = true;
                }
            }
            catch
            {
                throw new Exception("Ошибка базы данных, попробуйте позже.");
            }


        }

        /// <summary>
        /// Событие при клике на кнопку "BBK"
        ///</summary>
        private void RadioButtonBbk_Click(object sender, RoutedEventArgs e)
        {
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_bbk";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";
        }

        /// <summary>
        /// Событие при клике на кнопку "Название"
        ///</summary>
        private void RadioButtonName_Click(object sender, RoutedEventArgs e)
        {
            BBkInputComboBox.ItemsSource = fieldsController.GetBbk();
            BBkInputComboBox.DisplayMemberPath = "field_knowledge_name";
            BBkInputComboBox.SelectedValuePath = "field_knowledge_id";
        }

        /// <summary>
        /// Событие при клике на кнопку "Вести вручную"
        /// </summary>
        private void DirectInputBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectInputBtn.Visibility = Visibility.Visible;
            DirectInputBtn.Visibility = Visibility.Collapsed;
            DirectInputTextBox.Visibility = Visibility.Visible;
            BBkInputComboBox.Visibility = Visibility.Collapsed;
            SelectShowDocPanel.Visibility = Visibility.Collapsed;
            BbkSearchBox.Visibility = Visibility.Collapsed;
            BbkPlaceholderTextBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Событие при клике на кнопку "Вернуться к списку"
        /// </summary>
        private void SelectInputBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectInputBtn.Visibility = Visibility.Collapsed;
            DirectInputBtn.Visibility = Visibility.Visible;
            DirectInputTextBox.Visibility = Visibility.Collapsed;
            BBkInputComboBox.Visibility = Visibility.Visible;
            SelectShowDocPanel.Visibility = Visibility.Visible;
            BbkSearchBox.Visibility = Visibility.Collapsed;
            BbkPlaceholderTextBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Событие при вводе текста в поле "AuthorInput"
        /// </summary>
        private void AuthorInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckName(AuthorInput.Text);
            if (!trigger)
            {
                AuthorWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AuthorWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "NameInput"
        /// </summary>
        private void NameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckBookName(NameInput.Text);
            if (!trigger)
            {
                NameWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                NameWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "ISBNInput"
        /// </summary>
        private void ISBNInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckBookIsbn(ISBNInput.Text);
            if (!trigger)
            {
                ISBNWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                ISBNWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "YearInput"
        /// </summary>
        private void YearInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringCheck check = new StringCheck();

            bool trigger = check.CheckBookYear(YearInput.Text);
            if (!trigger || Convert.ToInt32(YearInput.Text) < 1500 || Convert.ToInt32(YearInput.Text) > DateTime.Now.Year)
            {
                YearWarningBtn.Visibility = Visibility.Visible;
            }
            else
            {
                YearWarningBtn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Событие при вводе текста в поле "DirectInputTextBox"
        /// </summary>
        private void DirectInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<string> zxc = fieldsController.GetBbkNumbers();
            string word = DirectInputTextBox.Text;

            if (zxc.Contains(word))
            {
                BbkWarningBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                BbkWarningBtn.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Событие при клике на кнопку "Продолжить"
        /// </summary>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringCheck check = new StringCheck();
            string resultString = "";

            bool resultAuthor = check.CheckName(AuthorInput.Text);
            if (!resultAuthor || AuthorInput.Text == "")
            {
                resultString += "Неправильно введено имя Автора\n";
            }

            bool resultName = check.CheckBookName(NameInput.Text);
            if (!resultName || NameInput.Text == "")
            {
                resultString += "Неправильно введено название\n";
            }

            string resultBbk = BBkInputComboBox.Text;
            string resultDirectBbk = DirectInputTextBox.Text;
            if (resultBbk == "" && resultDirectBbk == "")
            {
                resultString += "Неправильно введено BBK\n";
            }

            bool resultIsbn = check.CheckBookIsbn(ISBNInput.Text);
            if (!resultIsbn || ISBNInput.Text == "")
            {
                resultString += "Неправильно введен ISBN\n";
            }

            bool resultYear = check.CheckBookYear(YearInput.Text);
            if (!resultYear || YearInput.Text == "" || Convert.ToInt32(YearInput.Text) < 1500 || Convert.ToInt32(YearInput.Text) > DateTime.Now.Year)
            {
                resultString += "Неправильно введен год\n";
            }

            string resultInterpretor = InterpreterComboBox.Text;
            if (resultInterpretor == "")
            {
                resultString += "Неправильно введено имя Издания\n";
            }

            string resultChamber = ChamberComboBox.Text;
            if (resultChamber == "")
            {
                resultString += "Неправильно введен номер отсека\n";
            }

            if (resultString == "")
            {
                int userBbk = 0;

                if (SelectInputBtn.Visibility == Visibility.Collapsed)
                {
                    BbkWarningBtn.Visibility = Visibility.Collapsed;
                    userBbk = Convert.ToInt32(BBkInputComboBox.SelectedValue);
                }
                else if (SelectInputBtn.Visibility == Visibility.Visible)
                {
                    userBbk = fieldsController.GetBbkId(DirectInputTextBox.Text);
                }

                if (booksController.AddNewBook(AuthorInput.Text, userBbk, NameInput.Text, ISBNInput.Text, PlaceInput.Text, Convert.ToInt32(YearInput.Text), interpretorsController.GetInterpretorId(InterpreterComboBox.Text), Convert.ToInt32(ChamberComboBox.Text)))
                {
                    if(quantityController.AddNewQuantity(Settings.Default.bookId))
                    {
                        if (booksController.UpdateBookQuantity(Settings.Default.quantityId))
                        {
                            MessageBox.Show("Книга добавлена");
                            if (Settings.Default.role == 1)
                            {
                                this.NavigationService.Navigate(new MenuAdminPage());
                            }
                            if (Settings.Default.role == 2)
                            {
                                this.NavigationService.Navigate(new MenuLibrarianPage());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка базы данных, попробуйте позже.");
                    }
                }
            }
            else
            {
                MessageBox.Show(resultString);
            }
            
        }
    }
}
