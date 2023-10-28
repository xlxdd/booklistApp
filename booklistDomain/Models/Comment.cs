using booklistDomain.Entities.Identity;
using booklistDomain.Models;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Entities
{
    public record Comment:BaseEntity
    {
        public string Content { get; private set; }//评论内容
        public int LikeNum { get; private set; } = 0;//like数量
        public Guid CommentatorId { get; private set; }
        public Guid BookListId { get; private set; }
        private Comment() { } 
        public Comment ChangeLikeNum(int num)
        {
            this.LikeNum += num;
            return this;
        }
    }
}
