using Common.Models;

namespace booklistDomain.Entities
{
    public record BookList : BaseEntity, ISoftDelete
    {
        public Uri CoverUrl { get; private set; }//封面
        public string Title { get; private set; }//标题
        public string Descrpition { get; private set; }//简介
        public int CommentNum { get; private set; } = 0;//评论数量
        public int StarNum { get; private set; } = 0;//收藏数量
        public bool IsDeleted { get; private set; } = false;//软删除
        public Guid CreaterId { get; private set; }
        private BookList() { }
        public static BookList Create(Uri coverUrl, string title, string description, Guid id)
        {
            var bookList = new BookList();
            bookList.CoverUrl = coverUrl;
            bookList.Title = title;
            bookList.Descrpition = description;
            bookList.CreaterId = id;
            return bookList;
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
