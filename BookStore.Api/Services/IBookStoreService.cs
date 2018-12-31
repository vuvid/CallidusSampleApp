using BookStore.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookDto = BookStore.Api.Dto.Book;
using AuthorDto = BookStore.Api.Dto.Author;

namespace BookStore.Api.Services
{
    public interface IBookStoreService
    {
        IEnumerable<AuthorDto> GetAuthors();
        IEnumerable<BookDto> GetBooks();
        BookDto GetBookById(int bookId);
        BookDto InsertBook(BookDto book);
        int DeleteBook(int bookId);
        BookDto UpdateBook(BookDto book);
    }
}
