using booklistAPI.Comments.Request;
using FluentValidation;

namespace booklistAPI.Stars.Request
{
    public class StarRequest
    {
        public Guid BookListId { get; private set; }//收藏书单id
    }
    public class StarRequestValidator : AbstractValidator<StarRequest>
    {
        public StarRequestValidator() { }
    }
}
