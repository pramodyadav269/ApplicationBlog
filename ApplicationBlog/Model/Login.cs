using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class Login
    {
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Password { get; set; }
    }
}
