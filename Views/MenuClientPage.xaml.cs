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
    /// Логика взаимодействия для MenuClientPage.xaml
    /// </summary>
    public partial class MenuClientPage : Page
    {
        Controllers.BooksController booksController = new Controllers.BooksController();

        public MenuClientPage()
        {
            InitializeComponent();
            Console.WriteLine("gggggggggg");
            //ClientDataGrid.ItemsSource = booksController.GetBookInfo();
        }

        private void FilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(FilterList.SelectedItem);

            if (Convert.ToInt32(FilterList.SelectedItem) ==1)
            {
                ClientDataGrid.ItemsSource = booksController.BooksInfoOutput();
            }


        }
    }
}
