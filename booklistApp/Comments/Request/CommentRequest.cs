using FluentValidation;

namespace booklistAPI.Comments.Request
{
    public class CommentRequest
    {
        public string Content { get; private set; }//评论内容
        public Guid BookListId { get; private set; }//被评论书单id
    }
    public class CommentRequestValidator:AbstractValidator<CommentRequest>
    {
        public CommentRequestValidator() { }
    }
}
