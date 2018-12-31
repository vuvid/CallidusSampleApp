using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Book = BookStore.Api.Models.Book;
using BookDto = BookStore.Api.Dto.Book;
using Author = BookStore.Api.Models.Author;
using AuthorDto = BookStore.Api.Dto.Author;

namespace BookStore.Api.Converters
{
    public interface IBookStoreConverter
    {
        Author Convert(AuthorDto source);
        AuthorDto Convert(Author source);
        Book Convert(BookDto source);
        BookDto Convert(Book source);
        Book Convert(BookDto source, Book destination);
        BookDto Convert(Book source, BookDto destination);
    }
}
