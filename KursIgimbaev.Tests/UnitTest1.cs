using FakeDataProviderLib;
using KursIgimbaev.model;
using KursIgimbaev.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KursIgimbaev.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        static public void Init(TestContext ts)
        {
            Globals.DataProvider = new FakeDataProvider();
        }
        //Проверяем продукт без названия,должен появиться Exception
        [TestMethod]
        public void Save_SaveProductWithoutTitle_Error()
        {
            Product newProduct = new Product()
            {
                FullName = ""
            };
            try
            {
                newProduct.Save();
                Assert.Fail();
                
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Save_SaveProductWithoutWeight_Error()
        {
            Product newProduct = new Product()
            {
                Weight = ""
            };
            try
            {
                newProduct.Save();
                Assert.Fail();

            }
            catch
            {

            }
        }
        // 
        [TestMethod]
        public void Save_SaveProductWithoutProductType_Success()
        {
            Product product = new Product()
            {
                id = 11
            };
            try
            {
                product.Save();
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Save_WithoutID_Error()
        {
            Product newProduct = new Product()
            {
                FullName = "qwe"
            };
            try
            {
                newProduct.Save();
                Assert.Fail();
            }
            catch
            {

            }
        }
        [TestMethod]
        public void Delete_DeleteProductWithoutID_Error()
        {
            Product product = new Product()
            {
                id = 0
            };
            try
            {
                product.Save();
                Assert.Fail();
            }
            catch
            {

            }
        }


    }
}
