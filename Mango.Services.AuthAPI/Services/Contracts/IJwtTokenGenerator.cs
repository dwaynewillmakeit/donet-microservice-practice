using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Services.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
