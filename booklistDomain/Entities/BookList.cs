using booklistDomain.Entities.Identity;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booklistDomain.Entities
{
    public record BookList:BaseEntity,ISoftDelete
    {
        public Uri CoverUrl { get; private set; }//封面
        public string Title { get; private set; }//标题
        public string Descrpition { get; private set; }//简介
        public int CommentNum { get; private set; } = 0;//评论数量
        public int StarNum { get; private set; } = 0;//收藏数量
        public bool IsDeleted { get; private set; } = false;//软删除
        public AppUser Creater { get; private set; }//书单和用户 一对多关系
        public IEnumerable<Book> Books { get; private set; } = new List<Book>();//书单和书 多对多关系
        public IEnumerable<Comment> Comments { get; private set; } = new List<Comment>();//书单和评论 一对多关系
        public IEnumerable<Star> Stars { get; private set; } = new List<Star>();//书单和收藏 一对多关系
        private BookList() { }
        public BookList Create(Uri coverUrl,string title,string description)
        {
            this.CoverUrl = coverUrl;
            this.Title = title;
            this.Descrpition = description;
            return this;
        }
        public BookList ChangeCover(Uri coverUrl)
        {
            this.CoverUrl = coverUrl;
            return this;
        }
        public BookList ChangeTitle(string title)
        {
            this.Title = title;
            return this;
        }
        public BookList ChangeDesc(string desc)
        {
            this.Descrpition = desc;
            return this;
        }
        public BookList ChangeStarNum(int num)
        {
            this.StarNum += num;
            return this;
        }
        public BookList ChangeCommentNum(int num)
        {
            this.CommentNum += num;
            return this;
        }
        public void SoftDelete()
        {
            this.IsDeleted = true;
        }
    }
}
