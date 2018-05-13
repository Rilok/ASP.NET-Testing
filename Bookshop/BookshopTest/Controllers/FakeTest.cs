using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookshop.Controllers;
using Bookshop.Data;
using Bookshop.Data.Fakes;
using Bookshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookshopTest.Controllers
{
    [TestClass]
    public class FakeTest
    {
        [TestMethod]
        public async Task CheckCreationOfStore_RedirectToActionResult()
        {
            var store = new Store();

            var bookRepo = new FakeBookRepository();
            var pubRepo = new FakePublisherRepository();
            var storeRepo = new FakeStoreRepository();

            var controller = new StoresController(bookRepo, pubRepo, storeRepo);

            var result = await controller.Create(store);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task CheckCreationOfStoreByWrongPerson_RedirectToModel()
        {
            var store = new Store();

            var bookRepo = new FakeBookRepository();
            var pubRepo = new FakePublisherRepository();
            var storeRepo = new FakeStoreRepository();

            var controller = new StoresController(bookRepo, pubRepo, storeRepo);
            controller.ModelState.AddModelError("", "pickAnything");

            var result = await controller.Create(store) as ViewResult;

            Assert.AreEqual(store, result.Model);
        }

        [TestMethod]
        public async Task DeleteStoreWhichDoesntExistInDatabase_ReturnNotFoundScreen()
        {
            var bookRepo = new FakeBookRepository();
            var pubRepo = new FakePublisherRepository();
            var storeRepo = new FakeStoreRepository();

            var controller = new StoresController(bookRepo, pubRepo, storeRepo);

            var result = await controller.Delete(1) as ViewResult;

            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}
