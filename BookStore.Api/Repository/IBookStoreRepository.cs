using BookStore.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Repository
{
    public interface IBookStoreRepository : IDisposable
    {
        void SyncBookAuthors(Book book, IEnumerable<BookAuthor>authors);
        IEnumerable<Author> GetAuthors();
        Author GetAuthorById(int id);
        IEnumerable<Book> GetBooks();
        Book GetBookById(int bookId);
        void InsertBook(Book book);
        void DeleteBook(int bookId);
        void UpdateBook(Book book);
        int Save();
    }
}
