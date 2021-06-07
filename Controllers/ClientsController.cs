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
    public class ClientsController
    {
        readonly DbHelper dbHelper = new DbHelper();

        public List<clients> ClientsInfoOutputWithOutAdmin()
        {
            return dbHelper.context.clients.Where(t => t.id_role != 1).ToList();
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

        public bool AddNewUser(string userName, string userSurname, string userPatronymic, DateTime userDate, string userAddress, string userWorkplace, string userStudyplace, string userPhone, string userLogin, string userPassword, string userEmail, string userTicket)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (!check.CheckName(userName) || !check.CheckName(userSurname) || !check.CheckName(userPatronymic) || !check.CheckDate(Convert.ToString(userDate))
                    || !check.CheckAddress(userAddress) || !check.CheckPhone(userPhone) || !check.CheckLogin(userLogin) || !check.CheckPassword(userPassword) || !check.CheckEmail(userEmail))
                {
                    return false;
                }

                else
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
                        email = userEmail,
                        ticket = userTicket
                    });

                    dbHelper.context.SaveChanges();
                    return true;
                    //}
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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

        public bool UpdateClientInfo(string newName, string newSurname, string newPatronymic, DateTime newBirthday, string newAddress, string newWorkplace, string newStudyplace, string newPhone, string newLogin, string newPassword, string newEmail, string newTicket, List<clients> oldUser)
        {
            try
            {
                StringCheck check = new StringCheck();

                if (!check.CheckName(newName) || !check.CheckName(newSurname) || !check.CheckName(newPatronymic) || !check.CheckDate(Convert.ToString(newBirthday))
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
                        item.birthday = newBirthday;
                        item.address = newAddress;
                        item.workplace = newWorkplace;
                        item.studyplace = newStudyplace;
                        item.phone = newPhone;
                        item.login = newLogin;
                        item.password = newPassword;
                        item.email = newEmail;
                        item.ticket = newTicket;
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

        public void SendInfo(string userLogin, string userPassword, string email)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
                Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                Smtp.EnableSsl = true;
                Smtp.Credentials = new NetworkCredential("enotik1enot@gmail.com", "890891506644Qq");
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("enotik1enot@gmail.com");
                Message.To.Add(new MailAddress(email));
                Message.Subject = "Данные для авторизации";
                Message.Body = "Ваши данные для авторизации:\nЛогин:" + userLogin + "Пароль:" + userPassword;

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
