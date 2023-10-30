using booklistAPI.Books.Request;
using booklistDomain.DomainService;
using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistDomain.Services;
using booklistInfrastructure;
using Microsoft.AspNetCore.Mvc;

namespace booklistAPI.Books
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IBookRepository _repository;
        private readonly BookDomainService _service;
        private readonly IFileService _fileService;
        public BookController(AppDbContext context, IBookRepository repository, BookDomainService service,IFileService fileService)
        {
            _context = context;
            _repository = repository;
            _service = service;
            _fileService = fileService;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book?>> FindBookById(Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            return Ok(book);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByPage([FromQuery]int page, [FromQuery] int size)
        {
            var books = await _repository.GetBooksAsync((page - 1) * size, size);
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByCategory(Guid id,[FromQuery] int page, [FromQuery] int size)
        {
            var books = await _service.FindBookByCTGR(id, (page - 1) * size, size);
            return Ok(books);
        }
        [HttpPost]
        public async Task<ActionResult> AddBook([FromForm]AddBookRequest req)
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
            foreach(var cid in req.Ctgrs)
            {
                var res = await _service.AddCTGRToBookAsync(book.Id, cid);
                _context.BookBookCtgrs.Add(res);
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteBookById(Guid id)
        {
            var book =await _repository.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound($"没有id={id}的book");
            }
            _context.Books.Remove(book);
            var res = await _repository.GetCTGRSByBookAsync(id);
            foreach(var record in res)
            {
                _context.BookBookCtgrs.Remove(record);
            }
            return Ok();
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> UpdateBookById(UpdateBookRequest req)
        {
            var book = await _repository.GetBookByIdAsync(req.id);
            if (book == null)
            {
                return NotFound($"没有id={req.id}的book");
            }
            var newurl =await _fileService.GetUrlOfImage(req.CoverImage);//获取新图片的地址
            await _fileService.DeleteImage(book.CoverUrl);//删除旧的图片
            book.ChangeCoverUrl(newurl);
            book.ChangeBookName(req.BookName);
            book.ChangeAuthor(req.Author);
            book.ChangePubName(req.PubName);
            book.ChangePubTime(req.PubTime);
            book.ChangePrice(req.Price);
            book.ChangeAbs(req.Abs);
            return Ok();
        }
    }
}
