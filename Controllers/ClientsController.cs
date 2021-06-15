using LibraryProject.Models;
using LibraryProject.Properties;
using StringCheckLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace LibraryProject.Controllers
{
    /// <summary>
    /// Класс для работы с данными таблицы "Clients"
    /// </summary>
    public class ClientsController
    {
        /// <summary>
        /// Обращение к контексту базы данных
        /// </summary>
        readonly DbHelper dbHelper = new DbHelper();

        /// <summary>
        /// Формирование листа со всеми книгами
        /// </summary>
        /// <returns>
        /// Лист со всеми книгами
        /// </returns>
        public List<clients> ClientsInfoOutputWithOutAdmin()
        {
            return dbHelper.context.clients.Where(t => t.id_role != 1).ToList();
        }

        /// <summary>
        /// Поиск совпадений полей name, surname, patronymic, ticket с вводимой строкой
        /// </summary>
        /// <param name="info" - строка, по которой ищутся совпадения></param>
        /// <returns>
        /// Лист с совпадениями
        /// </returns>
        public List<clients> ClientsMatchUpInfoOutput(string info)
        {
            return dbHelper.context.clients.Where(t => t.name.Contains(info) || t.surname.Contains(info) ||
                                                  t.patronymic.Contains(info) || t.ticket.Contains(info)).ToList();
        }

        /// <summary>
        /// Формирование листа с данными об авторизованном пользователе
        /// </summary>
        /// <param name="login" - логин авторизованного пользователя></param>
        /// <returns>
        /// Лист с данными об авторизированном пользователе
        /// </returns>
        public List<clients> ClientsLoginMatchUp(string login)
        {
            return dbHelper.context.clients.Where(t => t.login == login).ToList();
        }

        /// <summary>
        /// Формирование листа пользователей, которым выданы книги
        /// </summary>
        /// <returns>
        /// Лист с пользователями, которым выданы книги
        /// </returns>
        public List<clients> GetClientsWithTrading()
        {
            return dbHelper.context.clients.Where(t => t.id_trading != null).ToList();
        }

        /// <summary>
        /// Проверка логина и пароля при авторизации
        /// </summary>
        /// <param name="userLogin" - логин, вводимы при авторизации></param>
        /// <param name="userPassword" - пароль, вводимы при авторизации></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
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

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="userName" - имя></param>
        /// <param name="userSurname" - фамилия></param>
        /// <param name="userPatronymic" - отчество></param>
        /// <param name="userDate" - дата  рождения></param>
        /// <param name="userAddress" - адрес></param>
        /// <param name="userWorkplace" - место работы></param>
        /// <param name="userStudyplace" - место учебы></param>
        /// <param name="userPhone" - телефон></param>
        /// <param name="userLogin" - логин></param>
        /// <param name="userPassword" - пароль></param>
        /// <param name="userEmail" - адрес электронной почты></param>
        /// <param name="userTicket" - читательский юилет></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddNewUser(string userName, string userSurname, string userPatronymic, DateTime userDate, string userAddress, string userWorkplace, string userStudyplace, string userPhone, string userLogin, string userPassword, string userEmail, string userTicket)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (!check.CheckName(userName) || !check.CheckName(userSurname) || !check.CheckName(userPatronymic) || !check.CheckDate(Convert.ToString(userDate.Date.ToString("yyyy.MM.dd")))
                    || !check.CheckAddress(userAddress) || !check.CheckPhone(userPhone) || !check.CheckLogin(userLogin) || !check.CheckPassword(userPassword) || !check.CheckEmail(userEmail))
                {
                    return false;
                }

                else
                {
                    DateTime dateTime = Convert.ToDateTime(userDate.ToString("yyyy.MM.dd"));
                    dbHelper.context.clients.Add(new clients
                    {
                        id_trading = null,
                        id_role = 3,
                        name = userName,
                        surname = userSurname,
                        patronymic = userPatronymic,
                        birthday = dateTime,
                        address = userAddress,
                        workplace = userWorkplace,
                        studyplace = userStudyplace,
                        phone = userPhone,
                        login = userLogin,
                        password = userPassword,
                        email = userEmail,
                        ticket = userTicket
                    });

                    dbHelper.context.SaveChanges();
                    return true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Проверка на пользователя с таким же логином, что и при регистрации
        /// </summary>
        /// <param name="userLogin" - введенный логин при регистрации></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
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

        /// <summary>
        /// Обновление данных о пользователе
        /// </summary>
        /// <param name="newName" - новое имя></param>
        /// <param name="newSurname" - новоая фамилия></param>
        /// <param name="newPatronymic" - новое отчество></param>
        /// <param name="newBirthday" - новая дата рождения></param>
        /// <param name="newAddress" - новый адрес></param>
        /// <param name="newWorkplace" - новое место работы></param>
        /// <param name="newStudyplace" - новое место учебы></param>
        /// <param name="newPhone" - новый телефон></param>
        /// <param name="newLogin" - новый телефон></param>
        /// <param name="newPassword" - новый пароль></param>
        /// <param name="newEmail" - новый адрес электронной почты></param>
        /// <param name="oldUser" - лист со старыми данными пользователя></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool UpdateClientInfo(string newName, string newSurname, string newPatronymic, DateTime newBirthday, string newAddress, string newWorkplace, string newStudyplace, string newPhone, string newLogin, string newPassword, string newEmail, List<clients> oldUser)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (!check.CheckName(newName) || !check.CheckName(newSurname) || !check.CheckName(newPatronymic) || !check.CheckDate(Convert.ToString(newBirthday.ToString("yyyy.MM.dd")))
                    || !check.CheckAddress(newAddress) || !check.CheckPhone(newPhone) || !check.CheckLogin(newLogin) || !check.CheckPassword(newPassword))
                {
                    return false;
                }
                else
                {
                    foreach (var item in oldUser)
                    {
                        item.name = newName;
                        item.surname = newSurname;
                        item.patronymic = newPatronymic;
                        item.birthday = newBirthday.Date;
                        item.address = newAddress;
                        item.workplace = newWorkplace;
                        item.studyplace = newStudyplace;
                        item.phone = newPhone;
                        item.login = newLogin;
                        item.password = newPassword;
                        item.email = newEmail;
                    }

                    dbHelper.context.SaveChanges();
                    return true;
                }
            }
              
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Удаление выбранного пользователя
        /// </summary>
        /// <param name="selectString" - выбранная строка дата грид></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool DeleteClientInfo(clients selectString)
        {
            try
            {
                if(selectString == null)
                {
                    return false;
                }
                else
                {
                    var selectTrading = from search in dbHelper.context.clients
                                        where search.client_id == selectString.client_id
                                        select search;

                    if (selectTrading != null)
                    {
                        foreach (var item in selectTrading)
                        {
                            dbHelper.context.clients.Remove(item);
                        }
                        dbHelper.context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Отправление письма с авторизационными данными на почту
        /// </summary>
        /// <param name="userLogin" - логин нового пользователя></param>
        /// <param name="userPassword" - пароль нового пользователя></param>
        /// <param name="email" - адрес электронной почты нового пользователя></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool SendInfo(string userLogin, string userPassword, string email)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                Smtp.EnableSsl = true;
                Smtp.Credentials = new NetworkCredential("libraryautho.data@gmail.com", "QcNjxh472kc3rUN");
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("libraryautho.data@gmail.com");
                Message.To.Add(new MailAddress(email));
                Message.Subject = "Данные для авторизации";
                Message.Body = "Ваши данные для авторизации:\nЛогин:" + userLogin + "\nПароль:" + userPassword;

                Smtp.Send(Message);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Поиск читательского билета авторизированного пользователя
        /// </summary>
        /// <param name="userLogin" - логин авторизированного пользователя></param>
        /// <returns>
        /// Читательский билет авторизированного пользователя
        /// </returns>
        public string CheckTicketOnExistence(string userLogin)
        {
            string ticket = dbHelper.context.clients.Where(t => t.login == userLogin).FirstOrDefault().ticket;

            return ticket;
        }

        /// <summary>
        /// Поиск читательского билета авторизированного пользователя
        /// </summary>
        /// <param name="userLogin" - логин авторизированного пользователя></param>
        /// <returns>
        /// Читательский билет авторизированного пользователя
        /// </returns>
        public string GetTicketNumber(string userLogin)
        {
            return  dbHelper.context.clients.Where(t => t.login == userLogin).FirstOrDefault().ticket;
        }

        /// <summary>
        /// Добавление номера выдачи пользователю
        /// </summary>
        /// <param name="userLogin" - Логин авторизированного пользователя></param>
        /// <param name="newTradingId" - Добавляемый номер выдачи></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool AddTradingIdToCLient(string userLogin, int newTradingId)
        {
            try
            {
                foreach (var item in dbHelper.context.clients.Where(t => t.login == userLogin).ToList())
                {
                    item.id_trading = newTradingId;
                }

                dbHelper.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Удаление номеры выдачи у авторизированного пользователя
        /// </summary>
        /// <param name="userLogin" - логин авторизированного пользователя></param>
        /// <returns>
        /// true - в случае выполнения метода
        /// false - в случае не выполения метода
        /// </returns>
        public bool RemoveIdTradingFromClient(string userLogin)
        {
            try
            {
                foreach (var item in dbHelper.context.clients.Where(t => t.login == userLogin).ToList())
                {
                    item.id_trading = null;
                }

                dbHelper.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Обновление читательского билета у пользователя
        /// </summary>
        /// <param name="userLogin" - логин авторизированного пользователя></param>
        /// <param name="newTicket" - пароль авторизированного пользователя></param>
        /// <returns></returns>
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
