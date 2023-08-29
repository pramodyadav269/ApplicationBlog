using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class Users
    {
        [Key]
        public long UserId { get; set; }

        [Column(TypeName ="varchar(100)")]
        public string Username { get; set; }
        

        [Column(TypeName = "varchar(500)")]
        public string Password { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Firstname { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Lastname { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Mobile { get; set; }

        
        [Column(TypeName = "char(1)")]
        public char Gender { get; set; }
        public DateTime? DOB { get; set; }
        
        [Column(TypeName = "varchar(1000)")]
        public string ProfilePic { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string BackgroundPic { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string ProfileStatus { get; set; }
        public int CountryId { get; set; }        
        //public int StateId { get; set; }
        //public int CityId { get; set; }
        public DateTime? RegisteredOn { get; set; }
        public bool IsActive { get; set;}
    }
}
