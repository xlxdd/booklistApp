using booklistAPI.Identity.Request;
using booklistAPI.Identity.Response;
using booklistDomain.DomainService;
using booklistDomain.Entities.Identity;
using booklistDomain.Interfaces.Identity;
using booklistDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;

namespace booklistAPI.Identity
{
    [Route("[controller]/[action]")]
    [ApiController]
    [SwaggerTag("登陆相关接口")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityRepository _repository;
        private readonly IdentityDomainService _service;
        private readonly IMemoryCache _cache;
        private readonly ISMSService _sms;
        public IdentityController(IIdentityRepository repository, IdentityDomainService service, IMemoryCache cache, ISMSService sms)
        {
            _repository = repository;
            _service = service;
            _cache = cache;
            _sms = sms;
        }
        [HttpPost]
        [SwaggerOperation(Summary ="请求验证码")]
        public async Task<ActionResult> RequestCode([FromBody]string phoneNumber)
        {
            //先判断电话在不在缓存中
            var res = _cache.Get(phoneNumber);
            if (res != null)//如果在缓存说明已经请求过验证码了 无论有没有注册
            {
                return Ok("请稍后请求");
            }
            //如果不在缓存中 设置缓存
            var code = new Random().Next(1000,10000).ToString();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(120));
            _cache.Set(phoneNumber, code, cacheEntryOptions);
            await _sms.SendAsync(phoneNumber, code);
            return Ok();
        }
        [HttpPost]
        [SwaggerOperation(Summary = "通过手机号和验证码登陆")]
        public async Task<ActionResult<LoginResponse>> LoginWithCode([FromBody]LoginRequest req)
        {
            //先判断正确性
            var c = _cache.Get(req.phoneNumber);
            if (c == null || c.ToString() != req.code)
            {
                return NotFound();
            }
            //手机和验证码判断正确 就要判断是否注册了 没有注册过的用户会自动注册
            var user = await _repository.FindByPhoneNumberAsync(req.phoneNumber);
            if (user == null)//如果没有注册
            {
                user = AppUser.Create(req.phoneNumber);
                var res = await _repository.CreateAsync(user);//进行注册
                Console.WriteLine(res);
            }
            _cache.Remove(req.phoneNumber);//删除这一条缓存，让code只能用一次
            string token = await _service.BuildTokenAsync(user);
            var response = new LoginResponse();
            response.Token = token;
            response.RedirectUrl = "111";
            return Ok(response);
        }
        [HttpPost]
        [SwaggerOperation(Summary = "通过手机号和密码登陆")]
        public async Task<ActionResult<LoginResponse>> LoginWithPwd([FromBody]LoginPwdRequest req)
        {
            var user = await _repository.FindByPhoneNumberAsync(req.phoneNumber);
            if (user == null)//如果没有注册
            {
                return NotFound();
            }
            //注册了就检测密码是否正确
            if (await _repository.SignInWithPwd(user, req.pwd))
            {
                string token = await _service.BuildTokenAsync(user);
                var response = new LoginResponse();
                response.Token = token;
                response.RedirectUrl = "111";
                return Ok(response);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "设置密码")]
        public async Task<ActionResult> SetPwd([FromBody] string pwd)
        {
            var cur = await _repository.GetCurrentUserId(User);
            await _repository.SetPasswordAsync(cur, pwd);
            return Ok();
        }
        [HttpGet]
        [SwaggerOperation(Summary = "登出")]
        public IActionResult Logout()
        {
            return Redirect("index");
        }
    }
}
