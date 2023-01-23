using DatingApp.Entities;

namespace DatingApp.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public PhotoDto(int id, string url, bool isMain)
        {
            Id = id;
            Url = url;
            IsMain = isMain;
        }
        public PhotoDto(Photo entity)
        {
            Id = entity.Id;
            Url = entity.Url;
            IsMain = entity.IsMain;
        }
    }
}