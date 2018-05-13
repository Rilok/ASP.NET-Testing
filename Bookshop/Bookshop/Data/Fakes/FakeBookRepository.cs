using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookshop.Data.Interfaces;
using Bookshop.Models;

namespace Bookshop.Data
{
    public class FakeBookRepository : IBookRepository
    {
        public List<Book> Books = new List<Book>();

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await Task.Run(() => Books);
        }

        public async Task<IEnumerable<Book>> GetBooks(string bookName)
        {
            return await Task.Run(() => Books.Where(x => x.name.ToLower().Contains(bookName.ToLower())));
        }

        public async Task<Book> GetBook(int BookId)
        {
            return await Task.Run(() => Books.FirstOrDefault(x => x.id == BookId));
        }

        public bool BookExists(int id)
        {
            return Books.Any(e => e.id == id);
        }

        public bool BookExists(Book book)
        {
            return Books.Any(e => e.name == book.name);
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            Book book = Books.FirstOrDefault(x => x.id == bookId);
            Books.Remove(book);
        }

        public void UpdateBook(Book book)
        {
            var b = Books.FirstOrDefault(x => x.id == book.id);
            b = book;
        }

        public async Task<bool> Save()
        {
            return await Task.Run(() => true);
        }

    }
}
