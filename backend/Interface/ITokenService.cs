using DatingApp.Entities;

namespace DatingApp.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser appUser);
    }
}
