using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop.Models;

namespace Bookshop.Data.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetBooks(string bookName);
        Task<Book> GetBook(int bookId);
        Task<bool> Save();
        bool BookExists(int bookId);
        bool BookExists(Book book);
        void AddBook(Book book);
        void DeleteBook(int bookId);
        void UpdateBook(Book book);
    }
}
