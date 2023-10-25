using booklistDomain.Entities.Identity;
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
        public AppUser Commentator { get; private set; }//用户和评论 一对多关系
        public BookList BookList { get; private set; }//书单和评论 一对多关系
        public Like like { get; private set; }//评论和like 一对一关系
        private Comment() { }
        public Comment ChangeLikeNum(int num)
        {
            this.LikeNum += num;
            return this;
        }
    }
}
