using booklistDomain.Entities;
using booklistDomain.Models;

namespace booklistDomain.Interfaces
{
    public interface IBookListRepository
    {
        public Task<BookList?> GetBookListByIdAsync(Guid id);//通过id获取书单
        public Task<IEnumerable<BookList>> GetBookListByIdAsync(IEnumerable<Guid> ids);//批量获取
        public Task<IEnumerable<BookList>> GetBookListsAsync(int skipNum, int takeNum);//获取书单(分页)
        public Task<IEnumerable<BookList>> GetBookListsByUserAsync(Guid id);//获取指定用户创建的书单
        public Task<Comment?> GetCommentByIdAsync(Guid id);//获取指定id的评论
        public Task<IEnumerable<Comment>> GetCommentsByBookListAsync(Guid id);//获取指定书单的评论
        public Task<IEnumerable<Comment>> GetCommentsByUserAsync(Guid id);//获取指定用户的评论(这个好像一般不允许)
        public Task<Like?> GetLikeByIdAsync(long id);//获取指定id的like
        public Task<IEnumerable<Like>> GetLikesByCommentAsync(Guid id);//获取指定评论的所有like记录
        public Task<Star?> GetStarByIdAsync(long id);//获取指定id的收藏
        public Task<IEnumerable<Star>> GetStarsByBookList(Guid id);//获取指定书单的所有收藏记录
        public Task<IEnumerable<Star>> GetStarsByUserAsync(Guid id);//获取指定用户的收藏
        public Task<IEnumerable<BookBookList>> GetBooksByBookListAsync(Guid id);//获取指定书单中的书
    }
}
