using booklistDomain.DomainService;
using booklistDomain.Entities;
using booklistDomain.Interfaces;
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
        public BookController(AppDbContext context, IBookRepository repository, BookDomainService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Book?>> FindBookById(Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            return Ok(null);
        }
        [HttpGet]
        [Route("{page}/{size}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByPage(int page, int size)
        {
            var books = await _repository.GetBooksAsync((page - 1) * size, size);
            return Ok(books);
        }
        /*
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByCategory(Guid id)
        {
            
        }
        */
    }
}
