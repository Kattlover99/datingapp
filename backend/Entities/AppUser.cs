using DatingApp.DTOs;
using DatingApp.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public List<UserLike> LikedUsers { get; set; }
        public List<UserLike> LikedByUsers { get; set; }
        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesReceived { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        public AppUser()
        {
        }

        public AppUser(string userName)
        {
            UserName = userName;
        }

        public AppUser(MemberDto entity)
        {
            Id = entity.Id;
            UserName = entity.UserName;
            KnownAs = entity.KnownAs;
            Created = entity.Created;
            LastActive = entity.LastActive;
            Gender = entity.Gender;
            Introduction = entity.Introduction;
            LookingFor = entity.LookingFor;
            Interests = entity.Interests;
            City = entity.City;
            Country = entity.Country;
        }



        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }

        public override bool Equals(object obj)
        {
            return obj is AppUser user &&
                   Id == user.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
