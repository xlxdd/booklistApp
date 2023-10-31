using Common.Models;

namespace booklistDomain.Entities
{
    public record Comment : BaseEntity
    {
        public string Content { get; private set; }//评论内容
        public int LikeNum { get; private set; } = 0;//like数量
        public Guid CommentatorId { get; private set; }
        public Guid BookListId { get; private set; }
        private Comment() { }
        public static Comment Create(string content, Guid uid, Guid bid)
        {
            var comment = new Comment();
            comment.Content = content;
            comment.CommentatorId = uid;
            comment.BookListId = bid;
            return comment;
        }
        public Comment ChangeLikeNum(int num)
        {
            this.LikeNum += num;
            return this;
        }
    }
}
