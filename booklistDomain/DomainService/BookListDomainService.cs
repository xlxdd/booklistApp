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
            var comment = Comment.Create(
                content,
                uid,
                bid
                );
            var booklist =await _repository.GetBookListByIdAsync(bid);//获取被评论的书单
            if ( booklist == null )
            {
                throw new Exception($"booklistId={bid}不存在");
            }
            booklist.ChangeCommentNum(+1);
            return comment;
        }
        public async Task<Comment?> DeleteCommentAsync(Guid id,Guid uid)
        {
            var comment =await _repository.GetCommentByIdAsync(id);//找到这条评论
            if ( comment == null)
            {
                throw new Exception($"CommentId={id}不存在");
            }
            var booklist =await _repository.GetBookListByIdAsync(comment.BookListId);//找到这条评论所在的书单
            if ( booklist == null)
            {
                throw new Exception($"BookListId={comment.BookListId}不存在");
            }
            if (comment.CommentatorId == uid)
            {
                booklist.ChangeCommentNum(-1);
                return comment;
            }
            return null;
            
        }
        public async Task<Comment> DeleteCommentAdmin(Guid id)
        {
            var comment = await _repository.GetCommentByIdAsync(id);//找到这条评论
            if (comment == null)
            {
                throw new Exception($"CommentId={id}不存在");
            }
            var booklist = await _repository.GetBookListByIdAsync(comment.BookListId);//找到这条评论所在的书单
            if (booklist == null)
            {
                throw new Exception($"BookListId={comment.BookListId}不存在");
            }
            booklist.ChangeCommentNum(-1);
            return comment;
        }
        public async Task<Star> AddStarAsync(Guid bid, Guid sid)
        {
            var star = Star.Create(
                bid, 
                sid
                );
            var booklist = await _repository.GetBookListByIdAsync(bid);//获取被收藏的书单
            if (booklist == null)
            {
                throw new Exception($"booklistId={bid}不存在");
            }
            booklist.ChangeStarNum(+1);
            return star;
        }
        public async Task<Star> DeleteStarAsync(long id, Guid uid)
        {
            var star = await _repository.GetStarByIdAsync(id);//找到这条评论
            if (star == null)
            {
                throw new Exception($"StarId={id}不存在");
            }
            var booklist = await _repository.GetBookListByIdAsync(star.BookListId);//找到收藏的书单
            if (booklist == null)
            {
                throw new Exception($"BookListId={star.BookListId}不存在");
            }
            if (star.StarerId == uid)
            {
                booklist.ChangeStarNum(-1);
                return star;
            }
            return null;

        }
        public async Task<Star> DeleteStarAdmin(long id)
        {
            var star = await _repository.GetStarByIdAsync(id);//找到这条评论
            if (star == null)
            {
                throw new Exception($"StarId={id}不存在");
            }
            var booklist = await _repository.GetBookListByIdAsync(star.BookListId);//找到这条评论所在的书单
            if (booklist == null)
            {
                throw new Exception($"BookListId={star.BookListId}不存在");
            }
            booklist.ChangeStarNum(-1);
            return star;
        }
        public async Task<BookBookList> AddBookToList(Guid bid, Guid lid, bool del)
        {
            return await Task.FromResult(BookBookList.Create(bid, lid, del));
        }
        public Task<IEnumerable<BookList>> FindBookListByCreator(Guid id)
        {
            return _repository.GetBookListsByUserAsync(id);
        }
        public async Task<IEnumerable<BookList>> FindBookListByStar(Guid uid)
        {
            var stars = await _repository.GetStarsByUserAsync(uid);
            var ids = stars.Select(e => e.BookListId);
            var lists = await _repository.GetBookListByIdAsync(ids);
            return lists;
        }    
    }
}
