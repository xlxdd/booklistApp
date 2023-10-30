using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Models;

namespace booklistDomain.DomainService
{
    public class BookListDomainService
    {
        private readonly IBookListRepository _repository;
        public BookListDomainService(IBookListRepository repository)
        {
            _repository = repository;
        }
        public async Task<BookList> AddBookListAsync(Uri coverUrl, string title, string description, Guid id)
        {
            return await Task.FromResult(BookList.Create(
                coverUrl,
                title,
                description,
                id
                ));
        }
        public async Task<Comment> AddCommentAsync(string content, Guid uid, Guid bid)
        {
            return await Task.FromResult(Comment.Create(
                content, 
                uid, 
                bid
                ));
        }
        public async Task<Like> AddLikeAsync(Guid cid, Guid lid)
        {
            return await Task.FromResult(Like.Create(
                cid,
                lid
                ));
        }
        public async Task<Star> AddStarAsync(Guid bid, Guid sid)
        {
            return await Task.FromResult(Star.Create(
                bid,
                sid
                ));
        }
        public async Task<BookBookList> AddBookToList(Guid bid, Guid lid, bool del)
        {
            return await Task.FromResult(BookBookList.Create(bid, lid, del));
        }
        public async Task<IEnumerable<BookList>> FindStarBookListByUser(Guid uid)
        {
            var stars =await _repository.GetStarsByUserAsync(uid);
            var ids = stars.Select(e => e.BookListId);
            var lists = await _repository.GetBookListByIdAsync(ids);
            return lists;
        }
    }
}
