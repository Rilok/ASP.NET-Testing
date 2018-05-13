using Bookshop.Models;
using Bookshop.Data;
using Bookshop.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;


namespace BookshopTest.Controllers
{
    [TestClass]
    public class BooksControllerTests
    {
        [TestMethod]
        public async Task Index_ThreeEmptyBooksInDb_ReturnsCorrectAmount()
        {
            var books = new List<Book>()
            {
                new Book(),
                new Book(),
                new Book()
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBooks()).ReturnsAsync(books);
            var controller = new BooksController(service.Object);

            var result = await controller.Index(String.Empty);

            var viewResult = (ViewResult) result;
            var model = (viewResult).Model as List<Book>;
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public async Task Index_EmptyDb_ReturnsNoBooks()
        {
            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBooks()).ReturnsAsync(new List<Book>());
            var controller = new BooksController(service.Object);

            var result = await controller.Index(String.Empty);

            var viewResult = (ViewResult) result;
            var model = (viewResult).Model as List<Book>;
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public async Task Index_SearchSingleRecordFromString_ReturnsOneBookWithSameName()
        {
            var books = new List<Book>()
            {
                new Book { name = "Higurashi", genre = "Horror", year = 2015},
                new Book { name = "Wrobiona w magię", genre = "Fantasy", year = 2018},
                new Book { name = "Akame ga Kill", genre = "Walka", year = 2017}
            };

            var service = new Mock<IBookRepository>();

            service.Setup(x => x.GetBooks("Higurashi")).ReturnsAsync(new List<Book>() { books[0] });
            service.Setup(x => x.GetBooks("Wrobiona w magię")).ReturnsAsync(new List<Book>() { books[1] });
            service.Setup(x => x.GetBooks("Akame ga Kill")).ReturnsAsync(new List<Book>() { books[2] });

            var controller = new BooksController(service.Object);

            var result = await controller.Index("Higurashi");

            var viewResult = (ViewResult)result;
            var model = (viewResult).Model as List<Book>;
            Assert.AreEqual(books[0].name, model[0].name);
        }

        [TestMethod]
        public async Task Index_SearchTwoOrMoreRecordsFromString_ReturnsTwoOrMoreBooksWithSameName()
        {
            var books = new List<Book>()
            {
                new Book { name = "Higurashi", genre = "Horror", year = 2015},
                new Book { name = "Higurashi2", genre = "Fantasy", year = 2018},
                new Book { name = "Higurashi3", genre = "Walka", year = 2017}
            };

            var service = new Mock<IBookRepository>();

            service.Setup(x => x.GetBooks("Higurashi")).ReturnsAsync(new List<Book>() { books[0], books[1], books[2] });

            var controller = new BooksController(service.Object);

            var result = await controller.Index("Higurashi");

            var viewResult = (ViewResult)result;
            var model = (viewResult).Model as List<Book>;
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public async Task Details_BookExists_ReturnsBook()
        {
            var book1 = new Book()
            {
                name = "Higurashi",
                genre = "Horror",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Details(1);

            var viewResult = (ViewResult) result;
            var model = (viewResult).Model as Book;
            Assert.AreEqual(book1, model);
        }

        [TestMethod]
        public async Task Details_BookDoesntExist_ReturnsNotFoundView()
        {
            var book1 = new Book()
            {
                name = "Higurashi",
                genre = "Horror",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Details(1555);

            var viewResult = (ViewResult) result;
            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public void AddBook_ReturnsViewResult()
        {
            var service = new Mock<IBookRepository>();
            var controller = new BooksController(service.Object);

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task AddBook_ValidBook_ReturnsRedirectToActionResult()
        {
            var validBook = new Book()
            {
                name = "Higurashi",
                genre = "Horror",
                year = 2015
            };
            var service = new Mock<IBookRepository>();
            var controller = new BooksController(service.Object);

            var result = await controller.Create(validBook);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task AddBook_InvalidBook_ReturnsSameModel()
        {
            var invalidBook = new Book()
            {
                name = "costam",
                genre = "chcialbys",
                year = 1999
            };

            var service = new Mock<IBookRepository>();
            var controller = new BooksController(service.Object);

            var result = await controller.Create(invalidBook);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_BookIdExists_ReturnsSameBook()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Edit(1);

            var viewResult = (ViewResult) result;
            var model = (viewResult).Model as Book;

            Assert.AreEqual(book1, model);
        }

        [TestMethod]
        public async Task Edit_BookIdDoesntExist_ReturnsNotFoundView()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Edit(1555);

            var viewResult = (ViewResult)result;

            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task Edit_BookWithIdDoesntExist_ReturnsNotFoundView()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Edit(2137, book1);

            var viewResult = (ViewResult)result;

            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task Edit_BookExistsWithInvalidData_ReturnsSameModel()
        {
            var invalidBook = new Book()
            {
                id = 0,
                name = "costam",
                genre = "chcialbys",
                year = 1999
            };
            var books = new List<Book>() {invalidBook};

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(0)).ReturnsAsync(invalidBook);
            var controller = new BooksController(service.Object);
            controller.ModelState.AddModelError("PogieloCie", "TakSieNieRobi");

            var result = await controller.Edit(0, invalidBook);

            var viewResult = (ViewResult) result;
            var model = (viewResult).Model as Book;

            Assert.AreEqual(invalidBook, model);
        }

        [TestMethod]
        public async Task Edit_NewBookInfoValid_ReturnsRedirectToResultAction()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book1Changed = new Book()
            {
                name = "Higurashi",
                genre = "Horror",
                year = 2015
            };

            var service = new Mock<IBookRepository>();

            service.Setup(x => x.GetBook(0)).ReturnsAsync(book1);
            service.Setup(x => x.BookExists(0)).Returns(true);

            var controller = new BooksController(service.Object);

            var result = await controller.Edit(0, book1Changed);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_NewBookInfoInvalid_ReturnsObjectWithNewWrongData()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book1Changed = new Book()
            {
                name = "higurashi",
                genre = "horror",
                year = 2015
            };

            var service = new Mock<IBookRepository>();

            service.Setup(x => x.GetBook(0)).ReturnsAsync(book1);
            service.Setup(x => x.BookExists(0)).Returns(true);

            var controller = new BooksController(service.Object);
            controller.ModelState.AddModelError("Nope", "Głupotę walnąłeś");

            var result = await controller.Edit(0, book1Changed);

            var viewResult = (ViewResult) result;
            var model = (viewResult).Model as Book;

            Assert.AreEqual(book1Changed, model);
        }

        [TestMethod]
        public async Task Delete_BookIdExists_ReturnsSameBook()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Delete(1);

            var viewResult = (ViewResult)result;
            var model = (viewResult).Model as Book;

            Assert.AreEqual(book1, model);
        }

        [TestMethod]
        public async Task Delete_BookIdDoesntExist_ReturnsNotFoundView()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };
            var book2 = new Book()
            {
                name = "Wrobiona w magię",
                genre = "Fantasy",
                year = 2018
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            service.Setup(x => x.GetBook(2)).ReturnsAsync(book2);
            var controller = new BooksController(service.Object);

            var result = await controller.Delete(2137);

            var viewResult = (ViewResult)result;

            Assert.AreEqual("NotFound", viewResult.ViewName);
        }

        [TestMethod]
        public async Task RemoveBook_BookIdExists_ReturnsRedirectToResultAction()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            var controller = new BooksController(service.Object);

            var result = await controller.RemoveBook(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task RemoveBook_BookIdDoesntExist_ReturnsRedirectToResultAction()
        {
            var book1 = new Book()
            {
                name = "Higuraszki",
                genre = "Horrorek",
                year = 2015
            };

            var service = new Mock<IBookRepository>();
            service.Setup(x => x.GetBook(1)).ReturnsAsync(book1);
            var controller = new BooksController(service.Object);

            var result = await controller.RemoveBook(2137);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
 }
