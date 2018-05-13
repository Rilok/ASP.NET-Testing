using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bookshop.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookshopTest
{
    [TestClass]
    public class BookTests
    {
        private string _name, _genre;
        private int _year;

        [TestInitialize]
        public void InitializeTests()
        {
            _name = "Higurashi";
            _genre = "Horror";
            _year = 2015;
        }

        [TestMethod]
        public void AllBookData_isValid()
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
        public void NameGenre_notWithCapitalLetter()
        {
            var book = new Book()
            {
                name = "costam",
                genre = "jeszczejak",
                year = 1900
            };

            var result = TestModelHelper.Validate(book);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void NameGenre_LengthIsLessThanExpected()
        {
            var book = new Book()
            {
                name = "Co",
                genre = "Si",
            };

            var result = TestModelHelper.Validate(book);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]

        public void Year_TooFew()
        {
            var book = new Book()
            {
                name = "Elo",
                genre = "Ziomeczki",
                year = 1899
            };

            var result = TestModelHelper.Validate(book);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Year_NotFuture()
        {
            var book = new Book()
            {
                name = "Swietna",
                genre = "Futurystyczna",
                year = 2019
            };

            var result = TestModelHelper.Validate(book);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Year_CannotPutString()
        {
            var book = new Book()
            {
                name = new string('Z', 41),
                genre = new string('X', 41),
                year = _year
            };

            var result = TestModelHelper.Validate(book);
            Assert.AreEqual(2, result.Count);
        }

        [TestCleanup]
        public void CleanupTests()
        {
            _name = null;
            _genre = null;
            _year = 0;
        }
    }
}
