using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Models
{
    public record BookBookList
    {
        public long Id { get; init; }
        public Guid BookId { get; init; }
        public Guid BookListId { get; init; }
        public bool IsDeleted { get; private set; }
    }
}
