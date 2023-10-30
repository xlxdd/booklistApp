namespace booklistAPI.Books.Request
{
    public class UpdateBookRequest:AddBookRequest
    {
        public Guid id { get; private set; }
    }
}
