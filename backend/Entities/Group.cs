using System.ComponentModel.DataAnnotations;

namespace DatingApp.Entities
{
    public class Group
    {
        private string groupName;

        public Group()
        {
        }

        public Group(string groupName)
        {
            this.groupName = groupName;
        }

        [Key]
        public string Name { get; set; }
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}
