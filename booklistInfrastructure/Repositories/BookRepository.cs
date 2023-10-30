using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace booklistInfrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Book?> GetBookByIdAsync(Guid id)
        {
            return _context.Books.FirstOrDefaultAsync(b=>b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBookByIdAsync(IEnumerable<Guid> ids)
        {
            return await _context.Books.Where(b=>ids.Contains(b.Id)).ToListAsync();
        }

        public async Task<IEnumerable<BookCategory>> GetBookCategoriesAsync()
        {
            return await _context.BookCategories.ToListAsync();
        }

        public Task<BookCategory?> GetBookCategoryByIdAsync(Guid id)
        {
            return _context.BookCategories.FirstOrDefaultAsync(b=>b.Id == id);
        }

        public async Task<IEnumerable<BookCategory>> GetBookCategoryByIdAsync(IEnumerable<Guid> ids)
        {
            return await _context.BookCategories.Where(b => ids.Contains(b.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(int skipNum, int takeNum)
        {
            return await _context.Books.Skip(skipNum).Take(takeNum).ToListAsync();
        }

        public async Task<IEnumerable<BookBookList>> GetBooksByBookListAsync(Guid id)
        {
            return await _context.BookBookLists.Where(b=>b.BookListId==id).ToListAsync();
        }

        public async Task<IEnumerable<BookBookCategory>> GetBooksByCTGRAsync(Guid id, int skipNum, int takeNum)
        {
            return await _context.BookBookCtgrs.Where(b => b.BookCategoryId == id).Skip(skipNum).Take(takeNum).ToListAsync();
        }

        public async Task<IEnumerable<BookBookCategory>> GetCTGRSByBookAsync(Guid id)
        {
            return await _context.BookBookCtgrs.Where(b => b.BookId == id).ToListAsync();
        }
    }
}
