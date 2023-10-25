using Common.Models;

namespace booklistDomain.Entities
{
    public record Book : BaseEntity
    {
        public Uri CoverUri { get; private set; }
        public string BookName { get; private set; }
        public string Author { get; private set; }
        public string PubName { get; private set; }
        public DateTime PubTime { get; private set; }
        public decimal Price { get; private set; }
        public IEnumerable<BookCategory> BookCategories { get; private set; }
        public string Abstract { get ; private set; }
        //给EFcore使用的空构造函数
        private Book() { }

    }
}
