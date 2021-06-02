using LibraryProject.Models;
using LibraryProject.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace LibraryProject.Controllers
{
    public class ClientsController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<clients> ClientsInfoOutput()
        {
            return dbHelper.context.clients.ToList();
        }

        public List<clients> ClientsMatchUpInfoOutput(string info)
        {
            return dbHelper.context.clients.Where(t => t.name.Contains(info) || t.surname.Contains(info) ||
                                                  t.patronymic.Contains(info) || t.trading.ticket.Contains(info)).ToList();
        }

        public List<clients> ClientsPasswordMatchUp(string password)
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
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public void AddNewUser(string userName, string userSurname, string userPatronymic, DateTime userDate, string userAddress, string userWorkplace, string userStudyplace, string userPhone, string userLogin, string userPassword, string userTicket)
        {
            try
            {
                dbHelper.context.clients.Add(new clients
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
                    password = userPassword,
                    ticket = userTicket
                });

                dbHelper.context.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public bool CheckForAnExistingkUser(string userLogin)
        {
            try
            {
                int check = dbHelper.context.clients.Where(t => t.login == userLogin).ToList().Count();

                if (check == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UpdateClientInfo(string newName, string newSurname, string newPatronymic, string newAddress, string newWorkplace, string newStudyplace, string newPhone, string newLogin, string newPassword, List<clients> qwe)
        {
            try
            {
                foreach (var item in qwe)
                {
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteClientInfo(clients selectString)
        {
            try
            {
                dbHelper.context.clients.Remove(selectString);
                dbHelper.context.SaveChanges();
                MessageBox.Show("Удалена информация о" + selectString);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public void SendInfo(string userLogin, string userPassword)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                Smtp.EnableSsl = true;
                Smtp.Credentials = new NetworkCredential("", "");
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("");
                Message.To.Add(new MailAddress("skochkov.aleksey@yandex.ru"));
                Message.Subject = "Данные для авторизации";
                Message.Body = "Ваши данные для авторизации:\n Логин:" + userLogin + "Пароль:" + userPassword;

                Smtp.Send(Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string GetTicket(string userLogin)
        {
            return dbHelper.context.clients.Where(t => t.login == userLogin).FirstOrDefault().ticket;
        }

        public bool UpdateUserTicket(string userLogin, string newTicket)
        {
            try
            {
                var userList = dbHelper.context.clients.Where(t => t.login == userLogin).ToList();

                foreach (var item in userList)
                {
                    item.ticket = newTicket;
                }

                dbHelper.context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
