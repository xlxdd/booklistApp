using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Models
{
    public class BookBookCategory
    {
        public long Id { get; private set; }
        public Guid BookId { get; private set; }
        public Guid BookCategoryId { get; private set; }
        private BookBookCategory() { }
        public static BookBookCategory Create(Guid bid,Guid cid)
        {
            var bookBookCategory = new BookBookCategory();
            bookBookCategory.BookId = bid;
            bookBookCategory.BookCategoryId = cid;
            return bookBookCategory;
        }
    }
}
