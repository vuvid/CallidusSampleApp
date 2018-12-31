using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Models
{
    public class Book: BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }

        public decimal Price { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
