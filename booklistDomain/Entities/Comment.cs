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
        public AppUser Commentator { get; private set; }//用户和评论，一对多关系
        public BookList BookList { get; private set; }//书单和评论，一对多关系
        private Comment() { }
        //评论暂时设为不可修改
    }
}
