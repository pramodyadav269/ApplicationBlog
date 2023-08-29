using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class State
    {
        [Key]
        public long StateId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Statename { get; set; }
        public bool IsActive { get; set; }
    }
}
