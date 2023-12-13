using AuthApi.Dtos.Enteties;

namespace AuthApi.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
