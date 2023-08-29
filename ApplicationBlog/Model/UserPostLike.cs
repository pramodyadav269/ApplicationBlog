using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class UserPostLike
    {
        [Key]
        public long UserPostLikeId { get; set; }
        public long? UserId { get; set; }
        public long? UserPostId { get; set; }
        public DateTime? LikedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
