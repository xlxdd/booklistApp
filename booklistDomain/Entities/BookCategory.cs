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
        private BookCategory() { }
        public string Name { get;private set; }
    }
}
