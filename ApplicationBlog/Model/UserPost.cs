using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class UserPost
    {
        [Key]
        public long UserPostId { get; set; }
        public long? UserId { get; set; }
        public DateTime? PostedOn { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string PostType { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string PostText { get; set; }


        [Column(TypeName = "varchar(500)")]
        public string PostMediaPath { get; set; }

        public long? LikeCount { get; set; }
        public long? CommentCount { get; set; }

        public bool IsActive { get; set; }
    }
}
