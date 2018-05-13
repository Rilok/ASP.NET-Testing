using System.Threading.Tasks;
using Bookshop.Data;
using Bookshop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Bookshop.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BookshopTest.Controllers
{
    [TestClass]
    public class TestDoubleBook
    {
        private string _name, _genre;
        private int _year;

        [TestInitialize]
        public void initialize()
        {
            _name = "Higurashi";
            _genre = "Horror";
            _year = 2015;
        }

        [TestMethod]
        public void CheckValidatorsOnBook()
        {
            var book = new Book()
            {
                name = _name,
                genre = _genre,
                year = _year
            };

            var context = new ValidationContext(book);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(book, context, result, true);

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public async Task RedirectionAfterCreatingBook()
        {
            var book = new Book();
            var bookRepository = new FakeBookRepository();
            var controller = new BooksController(bookRepository);

            var result = await controller.Create(book);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task CheckIfBookIsAdded()
        {
            var book = new Book()
            {
                name = _name,
                genre = _genre,
                year = _year
            };

            var context = new FakeBookRepository();
            var controller = new BooksController(context);

            await controller.Create(book);

            Assert.AreEqual(1, context.Books.Count);

        }
    }
}
