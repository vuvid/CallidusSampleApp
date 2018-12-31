using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Api.Dto;
using Book = BookStore.Api.Models.Book;
using Author = BookStore.Api.Models.Author;
using BookDto = BookStore.Api.Dto.Book;
using AuthorDto = BookStore.Api.Dto.Author;

namespace BookStore.Api.Converters
{
    public class BookStoreConverter : IBookStoreConverter
    {

        public BookDto Convert(Book source)
        {
            return new BookDto() {
                Id = source.Id,
                Created = source.Created,
                Modified = source.Modified,
                Isbn = source.Isbn,
                Title = source.Title,
                Year = source.Year,
                Price = source.Price,
                Authors = source.BookAuthors?.Select(x => Convert(x.Author))
            };
        }

        public Book Convert(BookDto source)
        {
            return new Book() {
                Isbn = source.Isbn,
                Title = source.Title,
                Year = source.Year,
                Price = source.Price,
            };
        }

        public Book Convert(BookDto source, Book destination)
        {
            destination.Isbn = source.Isbn;
            destination.Title = source.Title;
            destination.Year = source.Year;
            destination.Price = source.Price;

            return destination;
        }

        public BookDto Convert(Book source, BookDto destination)
        {
            destination.Id = source.Id;
            destination.Created = source.Created;
            destination.Modified = source.Modified;
            destination.Isbn = source.Isbn;
            destination.Title = source.Title;
            destination.Year = source.Year;
            destination.Price = source.Price;

            return destination;
        }

        public Author Convert(AuthorDto source)
        {
            return new Author() {
                Id = source.Id,
                Name = source.Name,
                Birthday = source.Birthday
            };
        }

        public AuthorDto Convert(Author source)
        {
            return new AuthorDto() {
                Id = source.Id,
                Created = source.Created,
                Modified = source.Modified,
                Name = source.Name,
                Birthday = source.Birthday
            };
        }

    }

}
