using Common.Models;

namespace booklistDomain.Entities
{
    public record Book : BaseEntity
    {
        public Uri CoverUrl { get; private set; }
        public string BookName { get; private set; }
        public string Author { get; private set; }
        public string PubName { get; private set; }
        public DateTime PubTime { get; private set; }
        public decimal Price { get; private set; }
        public string Abs { get; private set; }
        //给EFcore使用的空构造函数
        private Book() { }
        public static Book Create(Uri coverUrl, string bookName, string author, string pubName, DateTime pubTime, decimal price, string abs)
        {
            var book = new Book();
            book.CoverUrl = coverUrl;
            book.BookName = bookName;
            book.Author = author;
            book.PubName = pubName;
            book.PubTime = pubTime;
            book.Price = price;
            book.Abs = abs;
            return book;
        }
        public Book ChangeCoverUrl(Uri url)
        {
            this.CoverUrl = url;
            return this;
        }
        public Book ChangeBookName(string name)
        {
            this.BookName = name;
            return this;
        }
        public Book ChangeAuthor(string author)
        {
            this.Author = author;
            return this;
        }
        public Book ChangePubName(string pubName)
        {
            this.PubName = pubName;
            return this;
        }
        public Book ChangePubTime(DateTime pubTime)
        {
            this.PubTime = pubTime;
            return this;
        }
        public Book ChangePrice(decimal price)
        {
            this.Price = price;
            return this;
        }
        public Book ChangeAbs(string abs)
        {
            this.Abs = abs;
            return this;
        }
    }
}
