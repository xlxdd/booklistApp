using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using booklistDomain.Entities.Identity;

namespace booklistDomain.Entities
{
    public record Star
    {
        public long Id { get; init; }
        public BookList bookList { get; init; }//书单和收藏 一对一关系
        public AppUser Starer { get; init; }//用户和收藏 一对多
        private Star() { }
    }
}
