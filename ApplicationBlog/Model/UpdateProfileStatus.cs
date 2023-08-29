using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class UpdateProfileStatus
    {
        [Required]        
        public string ProfileStatus { get; set; }
    }
}
