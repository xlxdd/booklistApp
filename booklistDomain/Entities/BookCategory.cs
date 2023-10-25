using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Entities
{
    public record BookCategory:BaseEntity
    {
        public string Name { get;private set; }
        public IEnumerable<Book> books { get; private set; }=new List<Book>();
        private BookCategory() { }
        public BookCategory Create(string Name)
        {
            var bookctg = new BookCategory();
            bookctg.Name = Name;
            return bookctg;
        }
        //bookcategory设置为不能修改
    }
}
