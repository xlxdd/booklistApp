using booklistDomain.Entities.Identity;
using booklistDomain.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace booklistInfrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public IdentityRepository(UserManager<AppUser> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task<AppUser?> FindByIdAsync(Guid id)
        {
            return _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
        }
        //根据Id获取用户
        public Task<AppUser?> FindByPhoneNumberAsync(string phoneNum)
        {
            return _userManager.Users.FirstOrDefaultAsync(e => e.PhoneNumber == phoneNum);
        }
        //根据手机号获取用户
        public Task<IdentityResult> CreateAsync(AppUser user)
        {
            return _userManager.CreateAsync(user);
        }
        //创建用户
        public async Task<IdentityResult?> SetPasswordAsync(Guid id, string password)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentException($"{id}的用户不存在");
            }
            if (!await _userManager.HasPasswordAsync(user))
            {
                var addPasswordResult = await _userManager.AddPasswordAsync(user, password);
                return addPasswordResult;
            }
            else
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    return IdentityResult.Failed();
                }
                var addPasswordResult = await _userManager.AddPasswordAsync(user, password);
                return addPasswordResult;
            }
        }
        //设置密码
        public Task<IdentityResult> AddToRoleAsync(string role)
        {
            var r = new Role(role);
            return _roleManager.CreateAsync(r);
        }
        //添加身份
        public Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return _userManager.GetRolesAsync(user);
        }
        //获取用户身份
        public Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            return _userManager.AddToRoleAsync(user, role);
        }
        //向用户添加身份
        public Task<bool> SignInWithPwd(AppUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }
        //密码登陆检查
        public async Task<Guid> GetCurrentUserId(ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            var userId = u.Id;
            return userId;
        }
    }
}