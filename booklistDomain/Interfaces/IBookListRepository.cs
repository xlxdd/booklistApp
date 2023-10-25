using booklistDomain.Entities;

namespace booklistDomain.Interfaces
{
    public interface IBookListRepository
    {
        /// <summary>
        /// 本仓库对应聚合booklist
        /// 操作 表booklist comment star
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<BookList>> GetAllBookListAsync();//获取所有书单
        public Task<BookList?> GetBookListByIdAsync(Guid id);//根据id获取书单
        public Task<IEnumerable<Comment>> GetCommentsByBLIdAsync(Guid id);//获取某一书单的所有评论
        public Task<IEnumerable<Star>> GetStarsByBLIdAsync(Guid id, int num);//获取某一书单的前n位收藏者
    }
}
