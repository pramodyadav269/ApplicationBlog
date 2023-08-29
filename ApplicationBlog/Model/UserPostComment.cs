using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class UserPostComment
    {
        [Key]
        public long UserPostCommentId { get; set; }
        public long? UserId { get; set; }
        public long? UserPostId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string CommentText { get; set; }
        public DateTime? CommentedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
