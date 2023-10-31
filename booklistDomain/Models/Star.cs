namespace booklistDomain.Models
{
    public record Star
    {
        public long Id { get; private set; }
        public Guid BookListId { get; private set; }
        public Guid StarerId { get; private set; }
        private Star() { }
        public static Star Create(Guid bid, Guid sid)
        {
            var star = new Star();
            star.BookListId = bid;
            star.StarerId = sid;
            return star;
        }
    }
}
