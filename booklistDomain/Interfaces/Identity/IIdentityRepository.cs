using booklistDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace booklistDomain.Interfaces.Identity
{
    public interface IIdentityRepository
    {
        Task<AppUser?> FindByIdAsync(Guid userId);//根据Id获取用户
        Task<AppUser?> FindByPhoneNumberAsync(string phoneNum);//根据手机号获取用户
        Task<IdentityResult> CreateAsync(AppUser user);//创建用户
        Task<IdentityResult?> SetPasswordAsync(Guid id, string password);//设置密码
        Task<IdentityResult> AddToRoleAsync(string role);//添加身份
        Task<IList<string>> GetRolesAsync(AppUser user);//获取用户身份
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);//向用户添加身份
        public Task<bool> SignInWithPwd(AppUser user, string password);//密码登陆检查
        public Task<Guid> GetCurrentUserId(ClaimsPrincipal user);//获取用户id
    }
}
