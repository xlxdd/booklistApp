using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using booklistDomain.Entities;
using booklistDomain.Entities.Identity;

namespace booklistDomain.Models
{
    public record Star
    {
        public long Id { get; init; }
        public Guid BookListId { get; init; }
        public Guid StarerId { get; init; }
        private Star() { }
    }
}
