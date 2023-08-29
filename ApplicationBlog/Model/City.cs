using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class City
    {
        [Key]
        public long CityId { get; set; }
        public long StateId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Cityname { get; set; }
        public bool IsActive { get; set; }
    }
}
