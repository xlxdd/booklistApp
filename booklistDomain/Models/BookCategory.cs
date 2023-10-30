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
        private BookCategory() { }
        public static BookCategory Create(string Name)
        {
            var bookctg = new BookCategory();
            bookctg.Name = Name;
            return bookctg;
        }
        public BookCategory ChangeName(string name)
        {
            this.Name = name;
            return this;
        }
    }
}
