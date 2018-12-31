using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Frontend.Dto
{

    public class Book: BaseEntity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }

        public decimal Price { get; set; }

        public List<Author> Authors { get; set; }
    }
}
