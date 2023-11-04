using Common.Models;

namespace booklistDomain.Entities
{
    public record Comment : BaseEntity
    {
        public string Content { get; private set; }//评论内容
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
        }    }
}
