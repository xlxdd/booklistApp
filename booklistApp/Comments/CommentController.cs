using booklistAPI.Comments.Request;
using booklistDomain.DomainService;
using booklistDomain.Entities;
using booklistDomain.Interfaces;
using booklistInfrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace booklistAPI.Comments
{
    [Route("[controller]/[action]")]
    [ApiController]
    [SwaggerTag("评论相关接口")]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IBookListRepository _repository;
        private readonly BookListDomainService _service;
        public CommentController(AppDbContext context, IBookListRepository repository, BookListDomainService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定id的评论")]
        public async Task<ActionResult<Comment?>> FindCommentById([FromRoute]Guid id)
        {
            var comment = await _repository.GetCommentByIdAsync(id);
            return Ok(comment);
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "获取指定书单的评论")]
        public async Task<ActionResult<IEnumerable<Comment>>> FindCommentsByBook([FromRoute]Guid id)
        {
            var comments = await _repository.GetCommentsByBookListAsync(id);
            return Ok(comments);
        }
        [HttpPost]
        [SwaggerOperation(Summary ="添加评论")]
        public async Task<ActionResult> AddCommentToBookList([FromBody] CommentRequest req)
        {
            var uid = Guid.Empty;
            var _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out uid);//获取当前用户id
            var comment = await _service.AddCommentAsync(
                req.Content, 
                uid, 
                req.BookListId
                );
            _context.Comments.Add(comment);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "删除评论")]
        public async Task<ActionResult> DeleteCommentById([FromRoute]Guid id)
        {
            var uid = Guid.Empty;
            var _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out uid);//获取当前用户id
            var comment =await _service.DeleteCommentAsync(id, uid);
            if(comment == null)
            {
                return NotFound("删除失败");
            }
            _context.Comments.Remove(comment);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "管理员删除评论，不需要校验身份是否符合")]
        public async Task<ActionResult> DeleteCommentAdmin([FromRoute]Guid id)
        {
            var comment = await _service.DeleteCommentAdmin(id);
            _context.Comments.Remove(comment);
            return Ok();
        }
    }
}
