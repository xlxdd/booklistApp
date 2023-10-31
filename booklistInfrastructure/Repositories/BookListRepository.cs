using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace booklistInfrastructure.Repositories
{
    public class BookListRepository : IBookListRepository
    {
        private readonly AppDbContext _context;
        public BookListRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<BookList?> GetBookListByIdAsync(Guid id)
        {
            return _context.BookList.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BookList>> GetBookListByIdAsync(IEnumerable<Guid> ids)
        {
            return await _context.BookList.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<IEnumerable<BookList>> GetBookListsAsync(int skipNum, int takeNum)
        {
            return await _context.BookList.Skip(skipNum).Take(takeNum).ToListAsync();
        }

        public async Task<IEnumerable<BookList>> GetBookListsByUserAsync(Guid id)
        {
            return await _context.BookList.Where(x => x.CreaterId == id).ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(Guid id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBookListAsync(Guid id)
        {
            return await _context.Comments.Where(x => x.BookListId == id).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserAsync(Guid id)
        {
            return await _context.Comments.Where(x => x.CommentatorId == id).ToListAsync();
        }

        public Task<Like?> GetLikeByIdAsync(long id)
        {
            return _context.Likes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Like>> GetLikesByCommentAsync(Guid id)
        {
            return await _context.Likes.Where(x => x.CommentId == id).ToListAsync();
        }

        public Task<Star?> GetStarByIdAsync(long id)
        {
            return _context.Stars.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Star>> GetStarsByBookList(Guid id)
        {
            return await _context.Stars.Where(x => x.BookListId == id).ToListAsync();
        }

        public async Task<IEnumerable<Star>> GetStarsByUserAsync(Guid id)
        {
            return await _context.Stars.Where(x => x.StarerId == id).ToListAsync();
        }
        public async Task<IEnumerable<BookBookList>> GetBooksByBookListAsync(Guid id)
        {
            return await _context.BookBookLists.Where(b => b.BookListId == id).ToListAsync();
        }
    }
}
