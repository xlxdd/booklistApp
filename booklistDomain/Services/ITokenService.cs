using booklistDomain.Helpers;
using System.Security.Claims;

namespace booklistDomain.Services
{
    public interface ITokenService
    {
        string BuildToken(IEnumerable<Claim> claims, JWTOptions options);
    }
}
