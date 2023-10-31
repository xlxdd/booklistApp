using booklistAPI.BookLists.Request;
using booklistAPI.Books.Request;
using booklistDomain.DomainService;
using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Services;
using booklistInfrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace booklistAPI.BookLists
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BookListController:ControllerBase
    {
        private readonly AppDbContext _context;
        private IBookListRepository _repository;
        private readonly BookListDomainService _service;
        private readonly IFileService _fileService;
        public BookListController(AppDbContext context,IBookListRepository repository,BookListDomainService service,IFileService fileService)
        {
            _context = context;
            _repository = repository;
            _service = service;
            _fileService = fileService;
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定id的书单")]
        public async Task<ActionResult<BookList?>> FindBookListById([FromRoute] Guid id)
        {
            var bookList = await _repository.GetBookListByIdAsync(id);
            return Ok(bookList);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "分页获取书单")]
        public async Task<ActionResult<IEnumerable<BookList>>> GetBookListByPage([FromQuery] int page, [FromQuery] int size)
        {
            var bookLists = await _repository.GetBookListsAsync((page - 1) * size, size);
            return Ok(bookLists);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定用户创建的书单")]
        public async Task<ActionResult<IEnumerable<BookList>>> GetBookListByCreator([FromRoute] Guid id)
        {
            var bookLists = await _service.FindBookListByCreator(id);
            return Ok(bookLists);
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定用户收藏的书单")]
        public async Task<ActionResult<IEnumerable<BookList>>> GetBookListByStar([FromRoute] Guid id)
        {
            var bookLists = await _service.FindBookListByStar(id);
            return Ok(bookLists);
        }
        [HttpPost]
        [SwaggerOperation(Summary = "新增书单")]
        public async Task<ActionResult> AddBookList([FromForm]BookListRequest req)
        {
            var imgurl = await _fileService.GetUrlOfImage(req.CoverImage);//获取图片
            var uid = Guid.Empty;
            var _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),out uid);//获取用户id
            var booklist = await _service.AddBookListAsync(
                imgurl,
                req.Title,
                req.Descrpition,
                uid
                );
            _context.BookList.Add(booklist);
            foreach(var bid in req.Books)
            {
                var res = await _service.AddBookToList(bid,booklist.Id,false);
                _context.BookBookLists.Add(res);
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "删除指定id的书单")]
        public async Task<ActionResult> DeleteBooklistById([FromRoute]Guid id)
        {
            var bookList = await _repository.GetBookListByIdAsync(id);
            if (bookList == null)
            {
                return NotFound($"没有id={id}的bookList");
            }
            _context.BookList.Remove(bookList);
            var res = await _repository.GetBooksByBookListAsync(id);
            foreach (var record in res)
            {
                _context.BookBookLists.Remove(record);
            }
            return Ok();
        }
        [HttpPost]
        [Route("{id}")]
        [SwaggerOperation(Summary = "更新指定id的书单")]
        public async Task<ActionResult> UpdateBookListById([FromRoute] Guid id, [FromBody] BookListRequest req)
        {
            var bookList = await _repository.GetBookListByIdAsync(id);
            if (bookList == null)
            {
                return NotFound($"没有id={id}的bookList");
            }
            var newurl = await _fileService.GetUrlOfImage(req.CoverImage);//获取新图片的地址
            await _fileService.DeleteImage(bookList.CoverUrl);//删除旧的图片
            bookList.ChangeCover(newurl);
            bookList.ChangeTitle(req.Title);
            bookList.ChangeDesc(req.Descrpition);
            //更新连接表
            var olds = await _repository.GetBooksByBookListAsync(id);//当前书单的旧记录
            _context.BookBookLists.RemoveRange(olds);
            foreach(var bid in req.Books)
            {
                await _service.AddBookToList(bid, bookList.Id, false);
            }//添加新记录
            return Ok();
        }
    }
}
