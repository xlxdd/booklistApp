using booklistInfrastructure;
using FluentValidation;

namespace booklistAPI.Books.Request
{
    public class AddBookRequest
    {
        public IFormFile CoverImage { get;  set; }
        public string BookName { get;  set; }
        public string Author { get;  set; }
        public string PubName { get;  set; }
        public DateTime PubTime { get;  set; }
        public decimal Price { get;  set; }
        public string Abs { get;  set; }
        public IEnumerable<Guid> Ctgrs { get;  set; }
    }
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator(AppDbContext ctx)
        {
            RuleFor(x => x.BookName).NotEmpty().MaximumLength(30).WithMessage("书名不能为空");
        }
    }
}
