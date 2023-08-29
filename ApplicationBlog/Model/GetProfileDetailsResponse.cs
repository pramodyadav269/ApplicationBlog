using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBlog.Model
{
    public class GetProfileDetailsResponse : Response
    {
        public long UserId { get; set; }
        public string Username { get; set; }       
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public char Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string ProfilePicPath { get; set; }
        public string BackgroundPic { get; set; }
        public string ProfileStatus { get; set; }
        public string RegisteredOn { get; set; }
        public string Countryname { get; set; }
    }
}
