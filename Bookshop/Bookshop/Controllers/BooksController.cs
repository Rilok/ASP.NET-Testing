using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bookshop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Bookshop.Models;

namespace Bookshop.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // Widok Book
        public async Task<IActionResult> Index(string search)
        {
            if (!String.IsNullOrEmpty(search))
                return View(await _bookRepository.GetBooks(search));
            else
                return View(await _bookRepository.GetBooks());
        }
        // Dodawanie książki - GET
        public IActionResult Create()
        {
            return View();
        }
        // Dodawanie ksiązki - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.AddBook(book);
                await _bookRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }
        // Szczegóły książki
        public async Task<IActionResult> Details(int id)
        {
           var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return View("NotFound");
            }

            return View(book);
        }
        // Edytowanie Książki
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return View("NotFound");
            }

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.id)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookRepository.UpdateBook(book);
                    await _bookRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_bookRepository.BookExists(book.id))
                        return NotFound();
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // Usuwanie Książki
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return View("NotFound");
            }

            return View(book);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var book = await _bookRepository.GetBook(id);
            _bookRepository.DeleteBook(id);
            await _bookRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}