using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Repository
{
    public class BookStoreRepository: IBookStoreRepository, IDisposable
    {
        private BookStoreContext _context;

        public BookStoreRepository(BookStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors;
        }

        public Author GetAuthorById(int id)
        {
            return _context.Authors.Find(id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(z => z.Author);
        }

        public Book GetBookById(int id)
        {
            return _context.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(z => z.Author)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public void InsertBook(Book book)
        {
            _context.Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            Book book = _context.Books.Find(bookId);
            if (book != null) {
                _context.Books.Remove(book);
            }
        }

        public void SyncBookAuthors(Book book, IEnumerable<BookAuthor>bookAuthors)
        {
            if (book.BookAuthors != null) {
                foreach (var ba in book.BookAuthors) {
                    _context.BookAuthors.Remove(ba);
                }

                this.Save();
            }

            book.BookAuthors = new List<BookAuthor>();

            if (bookAuthors?.Count() > 0) {
                foreach (var ba in bookAuthors) {
                    _context.BookAuthors.Add(ba);
                }
            }
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed) {
                if (disposing) {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
