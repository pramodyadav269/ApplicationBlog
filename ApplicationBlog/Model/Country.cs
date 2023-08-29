using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    
    public class Country
    {
        [Key]
        public long CountryId { get; set; }
        
        [Column(TypeName = "varchar(200)")]
        public string Countryname { get; set; }
        public bool IsActive { get; set; }
    }
}
