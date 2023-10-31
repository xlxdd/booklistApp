namespace booklistDomain.Models
{
    public record BookBookList
    {
        public long Id { get; private set; }
        public Guid BookId { get; private set; }
        public Guid BookListId { get; private set; }
        public bool IsDeleted { get; private set; }
        private BookBookList() { }
        public static BookBookList Create(Guid bid, Guid lid, bool del)
        {
            var bookBookList = new BookBookList();
            bookBookList.BookId = bid;
            bookBookList.BookListId = lid;
            bookBookList.IsDeleted = del;
            return bookBookList;
        }
    }
}
