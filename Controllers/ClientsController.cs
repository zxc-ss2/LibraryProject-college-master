using LibraryProject.Models;
using LibraryProject.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LibraryProject.Controllers
{
    public class ClientsController
    {
        DbHelper dbHelper = new DbHelper();

        public List<Models.clients> ClientsInfoOutput()
        {
            return dbHelper.context.clients.ToList();
        }

        public List<Models.clients> ClientsMatchUpInfoOutput(string info)
        {
            return dbHelper.context.clients.Where(t => t.name.Contains(info) || t.surname.Contains(info) ||
                                                  t.patronymic.Contains(info) || t.trading.ticket.Contains(info)).ToList();
        }

        public List<Models.clients> ClientsPasswordMatchUp(string password)
        {
            return dbHelper.context.clients.Where(t => t.password == password).ToList();
        }

        /// <summary>
        /// Проверка логина и пароля при авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckUser(string userLogin, string userPassword)
        {
            clients user = dbHelper.context.clients.AsNoTracking().FirstOrDefault(t => t.login == userLogin && t.password == userPassword);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return false;
            }

            Settings.Default.login = userLogin;
            Settings.Default.password = userPassword;
            Settings.Default.role = Convert.ToInt32(dbHelper.context.clients.Where(t => t.login == userLogin).First().id_role);
            return true;
        }

        public void AddNewUser(string userName, string userSurname, string userPatronymic, DateTime userDate, string userAddress, string userWorkplace, string userStudyplace, string userPhone, string userLogin, string userPassword)
        {
            dbHelper.context.clients.Add(new Models.clients
            {
                id_trading = null,
                id_role = 3,
                name = userName,
                surname = userSurname,
                patronymic = userPatronymic,
                birthday = userDate,
                address = userAddress,
                workplace = userWorkplace,
                studyplace = userStudyplace,
                phone = userPhone,
                login = userLogin,
                password = userPassword
            });

            dbHelper.context.SaveChanges();
        }

        public bool CheckForAnExistingkUser(string userLogin)
        {
            int check = dbHelper.context.clients.Where(t => t.login == userLogin ).ToList().Count();

            if (check == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован");
                return false;
            }
        }

        public bool UpdateClientInfo(string newName, string newSurname, string newPatronymic, string newAddress, string newWorkplace, string newStudyplace, string newPhone, string newLogin, string newPassword, List<Models.clients> qwe)
        {

            foreach (var item in qwe) { 
                item.name = newName;
                item.surname = newSurname;
                item.patronymic = newPatronymic;
                item.address = newAddress;
                item.workplace = newWorkplace;
                item.studyplace = newStudyplace;
                item.phone = newPhone;
                item.login = newLogin;
                item.password = newPassword;
            }

            dbHelper.context.SaveChanges();
            return true;
        }

        public void DeleteClientInfo(Models.clients selectString)
        {
            dbHelper.context.clients.Remove(selectString);
            dbHelper.context.SaveChanges();
            MessageBox.Show("Удалена информация о" + selectString);
        }
    }
}
