using DatingApp.Entities;

namespace DatingApp.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<PhotoDto> Photos { get; set; } = new List<PhotoDto>();

        public MemberDto()
        {
        }

        public MemberDto(int id, string userName, int age, string knownAs, DateTime created, DateTime lastActive, string gender, string introduction, string lookingFor, string interests, string city, string country, List<PhotoDto> photos)
        {
            Id = id;
            UserName = userName;
            Age = age;
            KnownAs = knownAs;
            Created = created;
            LastActive = lastActive;
            Gender = gender;
            Introduction = introduction;
            LookingFor = lookingFor;
            Interests = interests;
            City = city;
            Country = country;
            Photos = photos;
        }

        public MemberDto(AppUser entity)
        {
            Id = entity.Id;
            UserName = entity.UserName;
            Age = entity.GetAge();
            KnownAs = entity.KnownAs;
            Created = entity.Created;
            LastActive = entity.Created;
            Gender = entity.Gender;
            Introduction = entity.Introduction;
            LookingFor = entity.LookingFor;
            Interests = entity.Interests;
            City = entity.City;
            Country = entity.Country;
            Photos = entity.Photos.Select(x => new PhotoDto(x)).ToList();
            PhotoUrl = entity.Photos.FirstOrDefault()?.Url;
        }

    }
}
