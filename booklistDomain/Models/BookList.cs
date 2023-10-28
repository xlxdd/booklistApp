using booklistDomain.Entities.Identity;
using booklistDomain.Models;
using Common.Models;

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
        public Guid CreaterId { get; private set; }
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
