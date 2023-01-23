using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Entities
{
    [Table("Photos")]
    public class Photo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        //public Photo(PhotoDto entity)
        //{
        //    Id = entity.Id;
        //    Url = entity.Url;
        //    IsMain = entity.IsMain;
        //}

        public Photo(string url, bool isMain, string publicId)
        {
            Url = url;
            IsMain = isMain;
            PublicId = publicId;
        }

        public Photo()
        {
        }
    }
}