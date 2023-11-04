using booklistDomain.Entities.Identity;
using booklistDomain.Helpers;
using booklistDomain.Interfaces.Identity;
using booklistDomain.Services;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace booklistDomain.DomainService
{
    public class IdentityDomainService
    {
        private readonly IIdentityRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IOptions<JWTOptions> _optJWT;
        public IdentityDomainService(IIdentityRepository repository, ITokenService tokenService, IOptions<JWTOptions> opt)
        {
            _repository = repository;
            _tokenService = tokenService;
            _optJWT = opt;
        }
        public async Task<string> BuildTokenAsync(AppUser user)
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
