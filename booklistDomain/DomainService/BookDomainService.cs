using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Models;

namespace booklistDomain.DomainService
{
    public class BookDomainService
    {
        private readonly IBookRepository _repository;
        public BookDomainService(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<Book> AddBookAsync(Uri coverUrl, string bookName, string author, string pubName, DateTime pubTime, decimal price, string abs)
        {
            return await Task.FromResult(Book.Create(
                coverUrl,
                bookName,
                author,
                pubName,
                pubTime,
                price,
                abs));
        }
        public async Task<BookCategory> AddBookCategoryAsync(string name)
        {
            return await Task.FromResult(BookCategory.Create(name));
        }
        public async Task<BookBookCategory> AddCTGRToBookAsync(Guid bid, Guid cid)
        {
            return await Task.FromResult(BookBookCategory.Create(bid, cid));
        }
        public async Task<IEnumerable<Book>> FindBookByCTGR(Guid id, int skipNum, int takeNum)
        {
            var bookbookctgrs = await _repository.GetBooksByCTGRAsync(id, skipNum, takeNum);
            var ids = bookbookctgrs.Select(e => e.BookId);
            var books = await _repository.GetBookByIdAsync(ids);
            return books;
        }
        public async Task<IEnumerable<BookCategory>> FindCTGRByBook(Guid id)
        {
            var bookbookstgrs = await _repository.GetCTGRSByBookAsync(id);
            var ids = bookbookstgrs.Select(e => e.BookCategoryId);
            var ctgrs = await _repository.GetBookCategoryByIdAsync(ids);
            return ctgrs;
        }
        public async Task<IEnumerable<Book>> FindBookByBooList(Guid id)
        {
            var bookboolists = await _repository.GetBooksByBookListAsync(id);
            var ids = bookboolists.Select(e => e.BookId);
            var books = await _repository.GetBookByIdAsync(ids);
            return books;
        }
    }
}
