using booklistDomain.Entities.Identity;
using booklistDomain.Helpers;
using booklistDomain.Interfaces.Identity;
using booklistDomain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace booklistDomain.DomainService
{
    public class IdentityDomainService
    {
        private readonly IIdentityRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JWTOptions> _optJWT;
        public IdentityDomainService(IIdentityRepository repository,ITokenService tokenService,IOptions<JWTOptions> opt)
        {
            _repository = repository;
            _tokenService = tokenService;
            _optJWT = opt;
        }
        private async Task<SignInResult> CheckUserNameAndPwdAsync(string userName, string password)
        {
            var user = await _repository.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            //CheckPasswordSignInAsync会对于多次重复失败进行账号禁用
            var result = await _repository.CheckForSignInAsync(user, password, true);
            return result;
        }
        private async Task<SignInResult> CheckPhoneNumAndPwdAsync(string phoneNum, string password)
        {
            var user = await _repository.FindByPhoneNumberAsync(phoneNum);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            var result = await _repository.CheckForSignInAsync(user, password, true);
            return result;
        }

        //<(SignInResult Result, string? Token)>  元组的语法
        public async Task<(SignInResult Result, string? Token)> LoginByPhoneAndPwdAsync(string phoneNum, string password)
        {
            var checkResult = await CheckPhoneNumAndPwdAsync(phoneNum, password);
            if (checkResult.Succeeded)
            {
                var user = await _repository.FindByPhoneNumberAsync(phoneNum);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }
            else
            {
                return (checkResult, null);
            }
        }

        public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
        {
            var checkResult = await CheckUserNameAndPwdAsync(userName, password);
            if (checkResult.Succeeded)
            {
                var user = await _repository.FindByNameAsync(userName);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }
            else
            {
                return (checkResult, null);
            }
        }

        private async Task<string> BuildTokenAsync(AppUser user)
        {
            var roles = await _repository.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return _tokenService.BuildToken(claims, _optJWT.Value);
        }
    }
}
