using BookStore.Api.Converters;
using BookStore.Api.Models;
using BookStore.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Book = BookStore.Api.Models.Book;
using BookDto = BookStore.Api.Dto.Book;
using Author = BookStore.Api.Models.Author;
using AuthorDto = BookStore.Api.Dto.Author;
using Microsoft.Extensions.Caching.Memory;

namespace BookStore.Api.Services
{
    public class BookStoreService: IBookStoreService
    {
        private IBookStoreRepository _repository;
        private IBookStoreConverter _converter;
        private IMemoryCache _cache;

        public BookStoreService(IBookStoreRepository repository, IBookStoreConverter converter, IMemoryCache cache)
        {
            _repository = repository;
            _converter = converter;
            _cache = cache;
        }

        public int DeleteBook(Int32 bookId)
        {
            _repository.DeleteBook(bookId);
            return _repository.Save();
        }

        public BookDto GetBookById(Int32 bookId)
        {
            return _converter.Convert(_repository.GetBookById(bookId));
        }

        public IEnumerable<AuthorDto> GetAuthors()
        {
            return _cache.GetOrCreate("authorskey", (entry) => {
                entry.SetOptions(new MemoryCacheEntryOptions() {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(1)
                });
                return _repository.GetAuthors().OrderBy(x => x.Name).Select(x => _converter.Convert(x));
            });
        }

        public IEnumerable<BookDto> GetBooks()
        {
            return _repository.GetBooks()
                .OrderBy(x => x.Id)
                .Select(x => _converter.Convert(x));
        }

        public BookDto InsertBook(BookDto bookDto)
        {
            var book = _converter.Convert(bookDto);
            book.Created = DateTime.UtcNow;
            book.Modified = book.Created;
            _repository.InsertBook(book);
            _repository.Save();

            return _converter.Convert(book);
        }

        public BookDto UpdateBook(BookDto bookDto)
        {
            var book = _repository.GetBookById(bookDto.Id);

            var authors = bookDto.Authors?
                .Select(x => new Models.BookAuthor() {
                    Author = _repository.GetAuthorById(x.Id),
                    AuthorId = x.Id,
                    BookId = book.Id
                }).ToArray();

            book = _converter.Convert(bookDto, book);
            book.Modified = DateTime.UtcNow;

            _repository.SyncBookAuthors(book, authors);
            _repository.UpdateBook(book);
            _repository.Save();

            return _converter.Convert(book);
        }
    }
}
