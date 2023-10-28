using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Models
{
    public class BookBookCategory
    {
        public long Id { get; init; }
        public Guid BookId { get; init; }
        public Guid BookCategoryId { get; init; }
    }
}
