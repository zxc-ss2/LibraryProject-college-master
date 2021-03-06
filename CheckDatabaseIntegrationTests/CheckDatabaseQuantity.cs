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
    public class CheckDatabaseQuantity
    {
        DbHelper dbHelper = new DbHelper();
        readonly QuantityController quantityController = new QuantityController();

        [TestMethod]
        public void EditQuantityMinus_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 5;
            List<quantity> quantityList = new List<quantity>();
            string expectedName = "";

            //Act
            int oldQuantity = dbHelper.context.quantity.Where(t => t.book_id == book_id).FirstOrDefault().library_quantity;
            quantityList = quantityController.GetQuantity(book_id);
            if (quantityController.ChangeQuantityMinus(book_id, quantityList))
            {
                dbHelper = new DbHelper();
                int newQuantity = dbHelper.context.quantity.Where(t => t.book_id == book_id).FirstOrDefault().library_quantity;

                //Assert
                Assert.AreEqual(oldQuantity - 1, newQuantity);
            }
        }

        [TestMethod]
        public void EditQuantityMinus_InCorrectData_FalseReturned()
        {
            //Arrange
            int book_id = 0;
            List<quantity> quantityList = new List<quantity>();

            //Act
            quantityList = quantityController.GetQuantity(1);
            bool check = quantityController.ChangeQuantityMinus(book_id, quantityList);

            //Assert
            Assert.IsFalse(check);
            
        }

        [TestMethod]
        public void EditQuantityPlus_CorrectData_TrueReturned()
        {
            //Arrange
            int book_id = 5;
            List<quantity> quantityList = new List<quantity>();
            string expectedName = "";

            //Act
            int oldQuantity = dbHelper.context.quantity.Where(t => t.book_id == book_id).FirstOrDefault().library_quantity;
            quantityList = quantityController.GetQuantity(book_id);
            if (quantityController.ChangeQuantityPlus(book_id, quantityList))
            {
                dbHelper = new DbHelper();
                int newQuantity = dbHelper.context.quantity.Where(t => t.book_id == book_id).FirstOrDefault().library_quantity;

                //Assert
                Assert.AreEqual(oldQuantity + 1, newQuantity);
            }
        }

        [TestMethod]
        public void EditQuantityPlus_InCorrectData_FalseReturned()
        {
            //Arrange
            int book_id = 0;
            List<quantity> quantityList = new List<quantity>();

            //Act
            quantityList = quantityController.GetQuantity(1);
            bool check = quantityController.ChangeQuantityPlus(book_id, quantityList);

            //Assert
            Assert.IsFalse(check);
        }
    }
}
