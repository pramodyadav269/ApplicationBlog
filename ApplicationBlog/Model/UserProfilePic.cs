using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class UserProfilePic
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string ProfilePic { get; set; }
        public DateTime? RegisteredOn { get; set; }
        public bool IsActive { get; set; }
    }
}
