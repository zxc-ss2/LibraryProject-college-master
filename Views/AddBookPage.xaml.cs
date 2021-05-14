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
        public AddBookPage()
        {
            InitializeComponent();
        }

        Controllers.BooksController booksController = new Controllers.BooksController();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            booksController.AddNewBook(AuthorInput.Text, NameInput.Text, BBKInput.Text, ISBNInput.Text, PlaceInput.Text, Convert.ToInt32(YearInput.Text), Convert.ToInt32(AreaInput.Text), Convert.ToInt32(InterpretrInput.Text), Convert.ToInt32(ChamberInput.Text), Convert.ToInt32(TradingInput.Text));
        }
    }
}
