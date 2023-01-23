using Microsoft.AspNetCore.Identity;

namespace DatingApp.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRole { get; set; }
    }
}
