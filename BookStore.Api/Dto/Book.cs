using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Dto
{
    public class Book: BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<Author> Authors { get; set; }
    }
}
