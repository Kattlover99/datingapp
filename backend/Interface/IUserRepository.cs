using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;

namespace DatingApp.Interface
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<MemberDto> GetUserByIdAsync(int id);
        Task<AppUser> GetAppUserByIdAsync(int id);
        Task<MemberDto> GetUserByUsernameAsync(string username);
        bool UpdateUser(string username, MemberUpdateDto user);
        Task<AppUser> GetAppUserByUsernameAsync(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<bool> SaveAppUser(AppUser user);
        Task<string> GetUserGender(string username);
    }
}
