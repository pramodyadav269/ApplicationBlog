using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class Register
    {
        [Required]        
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public string DOB { get; set; }


        [Required]
        public string CountryId { get; set; }

        //[Required]
        //public string StateId { get; set; }

        //[Required]
        //public string CityId { get; set; }

        public string ProfilePic { get; set; }
    }
}
