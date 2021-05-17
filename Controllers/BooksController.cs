using LibraryProject.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryProject.Controllers
{
    public class BooksController
    {
        DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Models.books> BooksInfoOutput()
        {
            return dbHelper.context.books.ToList();
        }

        /// <summary>
        /// Проверка на совпадение вводимых данных с данными в бд
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public List<Models.books> BooksMatchUpInfoOutput(string author)
        {
            return dbHelper.context.books.Where(t => t.author.Contains(author) || t.isbn.Contains(author)).ToList();
        }

        public void AddNewBook(string bookAuthor, string bookName, string bookBBK, string bookISBN, string bookPlace, int bookYear, int bookKnowledgeField, int bookInterpreter, int bookChamber)
        {
            dbHelper.context.books.Add(new Models.books
            {
                author = bookAuthor,
                name = bookName,
                bbk = bookBBK,
                isbn = bookISBN,
                place = bookPlace,
                year = bookYear,
                knowledge_field_id = bookKnowledgeField,
                interpreter_id = bookInterpreter,
                chamber_id = bookChamber,
                trading_id = null
            });

            dbHelper.context.SaveChanges();
        }

        public void DeleteBookInfo(Models.books selectString)
        {
            dbHelper.context.books.Remove(selectString);
            dbHelper.context.SaveChanges();
            MessageBox.Show("Удалена информация о" + selectString);
        }

        //public void GetBookInfo(/*List selectDataGrid*/)
        //{
        //    SqlConnection thisConnection = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Database=library;Trusted_Connection=Yes;");
        //    thisConnection.Open();

        //    string sql = "select 'delivery', 'reception', 'author', 'name' from books inner join trading on books.book_id = trading.trading_id";

        //    SqlCommand cmd = thisConnection.CreateCommand();
        //    cmd.CommandText = sql;

        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable("emp");
        //    sda.Fill(dt);

        //    //selectDataGrid.ItemsSource = dt.DefaultView;
        //}

        public List<Models.books> GetTradingBooks()
        {
           return dbHelper.context.books.Where(t => t.trading.login == Settings.Default.login).ToList();
        }
    }
}
