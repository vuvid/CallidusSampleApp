using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Dto
{
    public class Author: BaseEntity
    {
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}
