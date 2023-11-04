using booklistDomain.DomainService;
using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistInfrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace booklistAPI.Categories
{
    [Route("[controller]/[action]")]
    [ApiController]
    [SwaggerTag("种类相关接口")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IBookRepository _repository;
        private readonly BookDomainService _service;
        public CategoryController(AppDbContext context, IBookRepository repository, BookDomainService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "获取所有种类")]
        public async Task<ActionResult<IEnumerable<BookCategory>>> GetAllCtgrs()
        {
            var ctgrs = await _repository.GetBookCategoriesAsync();
            return Ok(ctgrs);
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary ="获取指定id的书的种类")]
        public async Task<ActionResult<IEnumerable<BookCategory>>> GetCtgrsByBook([FromRoute]Guid id)
        {
            var ctgrs =await _service.FindCTGRByBook(id);
            return Ok(ctgrs);
        }
        [HttpPost]
        [SwaggerOperation(Summary = "添加种类")]
        public async Task<ActionResult> AddCategory([FromBody]string name)
        {
            var ctgr =await _service.AddBookCategoryAsync(name);
            _context.BookCategories.Add(ctgr);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "删除指定id的种类")]
        public async Task<ActionResult> DeleteCategoryById([FromRoute] Guid id)
        {
            var ctgr = await _repository.GetBookCategoryByIdAsync(id);
            if(ctgr == null)
            {
                return NotFound($"没有id={id}的ctgr");
            }
            _context.BookCategories.Remove(ctgr);
            var res = await _repository.GetBooksByCTGRAsync(id,0,int.MaxValue);//获取此种类的所有记录
            foreach (var record in res)
            {
                _context.BookBookCtgrs.Remove(record);
            }
            return Ok();
        }
        [HttpPost]
        [Route("{id}")]
        [SwaggerOperation(Summary ="更新指定id种类的名称")]
        public async Task<ActionResult> UpdateCtgrById([FromRoute]Guid id, [FromBody]string name)
        {
            var ctgr = await _repository.GetBookCategoryByIdAsync(id);
            if(ctgr == null)
            {
                return NotFound($"没有id为{id}的种类");
            }
            ctgr.ChangeName(name);
            return Ok();
        }
    }
}
