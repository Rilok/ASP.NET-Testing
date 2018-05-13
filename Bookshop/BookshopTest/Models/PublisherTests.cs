using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bookshop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookshopTest
{
    [TestClass]
    public class PublisherTests
    {
        private string _name, _street, _postalCode, _city, _phone;
        private int _streetNumber;

        [TestInitialize]
        public void InitializeTests()
        {
            _name = "Waneko";
            _street = "Orzeszkowa";
            _streetNumber = 33;
            _postalCode = "80-306";
            _city = "Gdańsk";
            _phone = "555 123 532";
        }

        [TestMethod]
        public void AllPublisherData_AreValid()
        {
            var publisher = new Publisher()
            {
                name = _name,
                street = _street,
                streetNumber = _streetNumber,
                postalCode = _postalCode,
                city = _city,
                phone = _phone
            };

            var context = new ValidationContext(publisher);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(publisher, context, result, true);

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void NameStreetCity_TooShort()
        {
            var publisher = new Publisher()
            {
                name = "O",
                street = "XDD",
                streetNumber = _streetNumber,
                postalCode = _postalCode,
                city = "W",
                phone = _phone
            };

            var result = TestModelHelper.Validate(publisher);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void NameStreetCity_TooLong()
        {
            var publisher = new Publisher()
            {
                name = new string('X', 31),
                street = new string('D', 41),
                streetNumber = _streetNumber,
                postalCode = _postalCode,
                city = new string('D', 31),
                phone = _phone
            };

            var result = TestModelHelper.Validate(publisher);
            Assert.AreEqual(3, result.Count);
        }
        [TestMethod]
        public void NameStreetCity_NotCapitalChar()
        {
            var publisher = new Publisher()
            {
                name = "insignis",
                street = "złomowa",
                streetNumber = _streetNumber,
                postalCode = _postalCode,
                city = "sosnowiec",
                phone = _phone
            };

            var result = TestModelHelper.Validate(publisher);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void StreetNumber_Wrong()
        {
            var publisher = new Publisher()
            {
                name = _name,
                street = _street,
                streetNumber = 0,
                postalCode = _postalCode,
                city = _city,
                phone = _phone
            };

            var result = TestModelHelper.Validate(publisher);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void PostalCode_Wrong()
        {
            var publisher = new Publisher()
            {
                name = _name,
                street = _street,
                streetNumber = _streetNumber,
                postalCode = "123-52",
                city = _city,
                phone = _phone
            };

            var result = TestModelHelper.Validate(publisher);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Phone_Wrong()
        {
            var publisher = new Publisher()
            {
                name = _name,
                street = _street,
                streetNumber = _streetNumber,
                postalCode = _postalCode,
                city = _city,
                phone = "12345656352"
            };

            var result = TestModelHelper.Validate(publisher);
            Assert.AreEqual(1, result.Count);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _name = null;
            _street = null;
            _streetNumber = 0;
            _postalCode = null;
            _city = null;
            _phone = null;
        }
    }
}
