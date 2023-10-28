using booklistDomain.Entities;
using booklistDomain.Models;

namespace booklistDomain.Interfaces
{
    public interface IBookRepository
    {
        public Task<Book?> GetBookByIdAsync(Guid id);//通过id获取书
        public Task<IEnumerable<Book>> GetBookByIdAsync(IEnumerable<Guid> ids);//批量获取
        public Task<IEnumerable<Book>> GetBooksAsync(int skipNum,int takeNum);//获取书(分页)
        public Task<BookCategory?> GetBookCategoryByIdAsync(Guid id);//通过id获取类别
        public Task<IEnumerable<BookCategory>> GetBookCategoryByIdAsync(IEnumerable<Guid> ids);//通过id获取类别
        public Task<IEnumerable<BookCategory>> GetBookCategoriesAsync();//获取所有类别
        public Task<IEnumerable<BookBookCategory>> GetCTGRSByBookAsync(Guid id);//获取书的类别
        public Task<IEnumerable<BookBookCategory>> GetBooksByCTGRAsync(Guid id);//通过类别获取书
        public Task<IEnumerable<BookBookList>> GetBooksByBookListAsync(Guid id);//获取指定书单中的书
    }
}
