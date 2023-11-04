using booklistAPI.Stars.Request;
using booklistDomain.DomainService;
using booklistDomain.Interfaces;
using booklistInfrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace booklistAPI.Stars
{
    [Route("[controller]/[action]")]
    [ApiController]
    [SwaggerTag("收藏")]
    public class StarController:ControllerBase
    {
        
        private readonly AppDbContext _context;
        private IBookListRepository _repository;
        private readonly BookListDomainService _service;
        public StarController(AppDbContext context, IBookListRepository repository, BookListDomainService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }
        [HttpPost]
        [SwaggerOperation(Summary = "添加收藏")]
        public async Task<ActionResult> AddStar([FromBody]StarRequest req) 
        {
            var uid = Guid.Empty;
            var _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out uid);//获取当前用户id
            var star = await _service.AddStarAsync(req.BookListId, uid);
            _context.Stars.Add(star);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "取消收藏")]
        public async Task<ActionResult> CancelSrarById([FromRoute] long id)
        {
            var uid = Guid.Empty;
            var _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out uid);//获取当前用户id
            var star = await _service.DeleteStarAsync(id, uid);
            if (star == null)
            {
                return NotFound("删除失败");
            }
            _context.Stars.Remove(star);
            return Ok();
        }
    }
}
