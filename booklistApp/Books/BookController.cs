using booklistAPI.Books.Request;
using booklistDomain.DomainService;
using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Services;
using booklistInfrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace booklistAPI.Books
{
    [Route("[controller]/[action]")]
    [ApiController]
    [SwaggerTag("书籍相关接口")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IBookRepository _repository;
        private readonly BookDomainService _service;
        private readonly IFileService _fileService;
        public BookController(AppDbContext context, IBookRepository repository, BookDomainService service, IFileService fileService)
        {
            _context = context;
            _repository = repository;
            _service = service;
            _fileService = fileService;
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定id的书")]
        public async Task<ActionResult<Book?>> FindBookById([FromRoute] Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            return Ok(book);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "分页获取书")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByPage([FromQuery] int page, [FromQuery] int size)
        {
            var books = await _repository.GetBooksAsync((page - 1) * size, size);
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "分页获取指定种类的书")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByCategory([FromRoute] Guid id, [FromQuery] int page, [FromQuery] int size)
        {
            var books = await _service.FindBookByCTGR(id, (page - 1) * size, size);
            return Ok(books);
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定书单中的书")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByList([FromRoute] Guid id)
        {
            var books = await _service.FindBookByBooList(id);
            return Ok(books);
        }
        [HttpPost]
        [SwaggerOperation(Summary = "新增书")]
        public async Task<ActionResult> AddBook([FromForm]BookRequest req)
        {
            var imgurl = await _fileService.GetUrlOfImage(req.CoverImage);
            var book = await _service.AddBookAsync(
                imgurl,
                req.BookName,
                req.Author,
                req.PubName,
                req.PubTime,
                req.Price,
                req.Abs);
            _context.Books.Add(book);
            foreach (var cid in req.Ctgrs)
            {
                var res = await _service.AddCTGRToBookAsync(book.Id, cid);
                _context.BookBookCtgrs.Add(res);
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "删除指定id的书")]
        public async Task<ActionResult> DeleteBookById([FromRoute]Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"没有id={id}的book");
            }
            _context.Books.Remove(book);
            var res = await _repository.GetCTGRSByBookAsync(id);
            foreach (var record in res)
            {
                _context.BookBookCtgrs.Remove(record);
            }
            return Ok();
        }
        [HttpPost]
        [Route("{id}")]
        [SwaggerOperation(Summary = "更新指定id的书")]
        public async Task<ActionResult> UpdateBookById([FromRoute]Guid id,[FromBody]BookRequest req)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"没有id={id}的book");
            }
            var newurl = await _fileService.GetUrlOfImage(req.CoverImage);//获取新图片的地址
            await _fileService.DeleteImage(book.CoverUrl);//删除旧的图片
            book.ChangeCoverUrl(newurl);
            book.ChangeBookName(req.BookName);
            book.ChangeAuthor(req.Author);
            book.ChangePubName(req.PubName);
            book.ChangePubTime(req.PubTime);
            book.ChangePrice(req.Price);
            book.ChangeAbs(req.Abs);
            var olds = await _repository.GetCTGRSByBookAsync(id);//当前书的旧记录
            _context.BookBookCtgrs.RemoveRange(olds);
            foreach (var cid in req.Ctgrs)
            {
                await _service.AddCTGRToBookAsync(book.Id,cid);
            }//添加新记录
            return Ok();
        }
    }
}
