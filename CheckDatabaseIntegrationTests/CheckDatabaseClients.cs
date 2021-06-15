using LibraryProject.Controllers;
using LibraryProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckDatabaseIntegrationTests
{
    [TestClass]
    public class CheckDatabaseClients
    {
        DbHelper dbHelper = new DbHelper();
        readonly ClientsController clientsController = new ClientsController();
        [TestMethod]
        public void AddUser_CorrectData_TrueReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышева 12";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "89505585644";
            string login = "user123";
            string password = "zxcQWE32!1";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";

            int oldClientsLength = dbHelper.context.clients.Count();
            int updatedClientsLength = 0;
            //Act
            if (clientsController.CheckForAnExistingkUser(login))
            {
                if (clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket))
                {
                    updatedClientsLength = dbHelper.context.clients.Count();
                    var selectString = dbHelper.context.clients.AsNoTracking().OrderByDescending(t => t.client_id).Take(1);
                    foreach (var item in selectString)
                    {
                        clientsController.DeleteClientInfo(item);
                    }


                    //Assert
                    Assert.AreEqual(oldClientsLength + 1, updatedClientsLength);
                }
            }

        }

        [TestMethod]
        public void AddExistsUser_CorrectData_FalseReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышева 12";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "89505585644";
            string login = "UliyaBay";
            string password = "zxcQWE32!1";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";

            //Act
            if (clientsController.CheckForAnExistingkUser(login))
            {
                bool check = clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket);

                //Assert
                Assert.IsFalse(check);
            }

        }

        [TestMethod]
        public void AddUser_EmptyData_FalseReturned()
        {
            //Arrange
            string name = "";
            string surname = "";
            string patronymic = "";
            string birthday = "2002.03.01";
            string address = "";
            string studyplace = "";
            string workplace = "";
            string phone = "";
            string login = "";
            string password = "!1";
            string email = "";
            string ticket = "";

            //Act
            if (clientsController.CheckForAnExistingkUser(login))
            {
                bool check = clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket);

                //Assert
                Assert.IsFalse(check);
            }
        }

        [TestMethod]
        public void AddUser_IncorrectData_FalseReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышев3243";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "895055857655644";
            string login = "user123";
            string password = "zxcQWE32!1";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";

            //Act
            if (clientsController.CheckForAnExistingkUser(login))
            {
                bool check = clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket);

                //Assert
                Assert.IsFalse(check);
            }
        }

        [TestMethod]
        public void EditUser_CorrectData_TrueReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышева 12";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "89505585644";
            string login = "User";
            string password = "zxcQWE!123";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";
            string newName = "Дмитрий";

            string expectedName = "";
            //Act
            List<clients> updatingClient = new List<clients>();

            if (clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket))
            {
                int updatedClientsLength = dbHelper.context.clients.Count();
                updatingClient = clientsController.ClientsLoginMatchUp("User");
                if (clientsController.UpdateClientInfo(newName, surname, patronymic, Convert.ToDateTime(birthday), address, studyplace, workplace, phone, login, password, email, updatingClient))
                {
                    dbHelper = new DbHelper();
                    expectedName = dbHelper.context.clients.Where(t => t.name == newName).FirstOrDefault().name;

                    var selectString = dbHelper.context.clients.AsNoTracking().OrderByDescending(t => t.client_id).Take(1);
                    foreach (var item in selectString)
                    {
                        clientsController.DeleteClientInfo(item);
                    }
                    //Assert
                    Assert.AreEqual(expectedName, newName);
                }
            }
        }

        [TestMethod]
        public void EditUser_InCorrectData_FalseReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышева 12";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "89505585644";
            string login = "User";
            string password = "zxcQWE!123";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";
            string newName = "23423423";

            //Act
            List<clients> updatingClient = new List<clients>();

            if (clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket))
            {
                int updatedClientsLength = dbHelper.context.clients.Count();
                bool check = clientsController.UpdateClientInfo(newName, surname, patronymic, Convert.ToDateTime(birthday), address, studyplace, workplace, phone, login, password, email, updatingClient);

                var selectString = dbHelper.context.clients.AsNoTracking().OrderByDescending(t => t.client_id).Take(1);
                foreach (var item in selectString)
                {
                    clientsController.DeleteClientInfo(item);
                }

                //Assert
                Assert.IsFalse(check);
            }
        }

        [TestMethod]
        public void EditUser_IsEmptyData_FalseReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышева 12";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "89505585644";
            string login = "User";
            string password = "zxcQWE!123";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";
            string newName = "";

            //Act
            List<clients> updatingClient = new List<clients>();

            if (clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket))
            {
                int updatedClientsLength = dbHelper.context.clients.Count();
                bool check = clientsController.UpdateClientInfo(newName, surname, patronymic, Convert.ToDateTime(birthday), address, studyplace, workplace, phone, login, password, email, updatingClient);

                var selectString = dbHelper.context.clients.AsNoTracking().OrderByDescending(t => t.client_id).Take(1);
                foreach (var item in selectString)
                {
                    clientsController.DeleteClientInfo(item);
                }

                //Assert
                Assert.IsFalse(check);
            }
        }

        [TestMethod]
        public void DeleteUser_CorrectData_TrueReturned()
        {
            //Arrange
            string name = "Андрей";
            string surname = "Петров";
            string patronymic = "Сергеевич";
            string birthday = "2002.03.01";
            string address = "Куйбышева 12";
            string studyplace = "ЕМК";
            string workplace = "";
            string phone = "89505585644";
            string login = "User";
            string password = "Fa1!enLeaves";
            string email = "andrey@gmail.com";
            string ticket = "Х-1234-21";
            string newName = "Дмитрий";

            int oldClientsLength = dbHelper.context.clients.Count();
            int updatedClientsLength = 0;
            //Act
            List<clients> updatingClient = new List<clients>();
            if (clientsController.CheckForAnExistingkUser("user123"))
            {
                if (clientsController.AddNewUser(name, surname, patronymic, Convert.ToDateTime(birthday), address, workplace, studyplace, phone, login, password, email, ticket))
                {
                    var selectString = dbHelper.context.clients.AsNoTracking().OrderByDescending(t => t.client_id).Take(1);
                    foreach (var item in selectString)
                    {
                        clientsController.DeleteClientInfo(item);
                    }

                    updatedClientsLength = dbHelper.context.clients.Count();
                    //Assert
                    Assert.AreEqual(oldClientsLength, updatedClientsLength);
                }
            }
        }


        [TestMethod]
        public void DeleteUser_NullData_FalseReturned()
        {
            //Arrange
            clients selectString = null;

            //Act
            bool check = clientsController.DeleteClientInfo(selectString);

            //Assert
            Assert.IsFalse(check);
        }
    }
}
