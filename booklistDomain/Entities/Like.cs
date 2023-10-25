using booklistDomain.Entities.Identity;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Entities
{
    public record Like
    {
        public long Id { get; init; }
        public Comment Comment { get; init; }//评论和like 一对一关系
        public AppUser Liker { get; init; }//用户和like 一对多
        private Like() { }
    }
}
